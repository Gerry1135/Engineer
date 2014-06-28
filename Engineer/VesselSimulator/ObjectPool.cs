// Kerbal Engineer Redux
// Author:  Padishar
// License: Attribution-NonCommercial-ShareAlike 3.0 Unported

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Engineer.VesselSimulator
{
    public interface IPooledObject
    {
        void FreePooled();
    }


    public class ObjectPool<T> where T : class, IPooledObject, new()
    {
        private const int chunkSize = 16;
        
        private List<T> freeObjects;
        private int currentFree;
        private HashSet<T> usedObjects;

        public ObjectPool()
        {
            freeObjects = new List<T>(chunkSize);
            currentFree = 0;
            usedObjects = new HashSet<T>();
        }

        public T Allocate()
        {
            T obj;
            if (currentFree > 0)
            {
                obj = freeObjects[--currentFree];
                freeObjects[currentFree] = null;
            }
            else
                obj = new T();

            usedObjects.Add(obj);
            return obj;
        }

        public void Free(T obj)
        {
            if (usedObjects.Remove(obj))
            {
                obj.FreePooled();

                if (currentFree == freeObjects.Capacity)
                    freeObjects.Capacity += chunkSize;

                freeObjects[currentFree++] = obj;
            }
        }
    }
}
