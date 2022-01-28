using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Random = UnityEngine.Random;

namespace Source.Scripts.Core
{
    public static class CoreUtils
    {
        public static T RandomEnumValue<T>(params T[] except) where T : Enum
        {
            var values = new List<T>((T[])Enum.GetValues(typeof(T)));
            
            foreach (var exceptValue in except)
            {
                values.Remove(exceptValue);
            }

            return values[Random.Range(0, values.Count)];
        }
        
        // Todo move to another utils file
        public static async Task ScheduleTask(Action action)
        {
            await Task.Factory.StartNew(action,
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }
        
        // Todo move to another utils file
        public static float MilliSecondsToSeconds(int milliseconds)
        {
            return milliseconds / 1000f;
        }
        
        // Todo move to another utils file
        public static float MilliSecondsToSeconds(long milliseconds)
        {
            return milliseconds / 1000f;
        }
        
        // Todo move to another utils file
        public static int SecondsToMilliseconds(float seconds)
        {
            return Mathf.RoundToInt(seconds * 1000);
        }
        
        public static string SafeAnyToString<T>(T value)
        {
            return value == null ? "" : value.ToString();
        }

        public struct LoadResult<T>
        {
            public T Value;
            public AsyncOperationStatus Status;
            public AsyncOperationHandle Handle;
        }

        public static async Task<LoadResult<T>> LoadAssetAsync<T>(AssetReference assetReference)
        {
            var handle = assetReference.LoadAssetAsync<T>();

            try
            {
                await handle.Task;
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            
            return new LoadResult<T>
            {
                Value = handle.Result,
                Status = handle.Status,
                Handle = handle
            };
        }

        public static void ResizeCollection<T>(int newSize, 
                                               Func<T> itemCreator,
                                               ICollection<T> collection,
                                               Action<T> itemDestroyer = null)
        {
            var size = collection.Count;

            if (newSize > size)
            {
                for (var i = size; i < newSize; i++)
                {
                    var item = itemCreator.Invoke();    
                    
                    collection.Add(item);
                }
            }
            else if (newSize == size)
            {
                return;
            }
            else
            {
                var deleteCount = size - newSize;

                foreach (var item in collection.ToList())
                {
                    if (deleteCount <= 0)
                    {
                        return;
                    }

                    if (itemDestroyer != null) itemDestroyer.Invoke(item);

                    collection.Remove(item);
                    
                    deleteCount--;
                }
            }
        }
    }
}