﻿// Copyright (c) 2022 Jonathan Lang

using System;
using System.Linq;
using System.Reflection;
using Baracuda.Monitoring.Interface;
using Baracuda.Reflection;

namespace Baracuda.Monitoring.Core.Utilities
{
    public abstract class OutParameterHandle
    {
        public abstract string GetValueAsString(object value);

        internal static OutParameterHandle CreateForType(Type type, IFormatData formatData)
        {
            var underlyingType = type.IsByRef ? type.GetElementType() : type;
            
#if ENABLE_IL2CPP
            if (underlyingType.IsReadonlyRefStruct())
            {
                return new OutParameterHandleRefStruct(type, formatData);
            }
#endif
            var concreteType = typeof(OutParameterHandleT<>).MakeGenericType(underlyingType);
            var ctor = concreteType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance).First();
            return (OutParameterHandle)ctor.Invoke(new object[] {formatData});
        }
    }
}