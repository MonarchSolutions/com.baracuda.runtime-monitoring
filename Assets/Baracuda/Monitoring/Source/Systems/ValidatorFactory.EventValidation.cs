﻿using System;
using System.Reflection;
using Baracuda.Monitoring.Source.Utilities;

namespace Baracuda.Monitoring.Source.Systems
{
    internal partial class ValidatorFactory
    {
        private ValidationEvent CreateEventValidatorInternal(MConditionalAttribute attribute, MemberInfo memberInfo)
        {
            if (attribute.ValidationMethod != ValidationMethod.ByMember)
            {
                return null;
            }

            var eventInfo = memberInfo.DeclaringType?.GetEvent(attribute.MemberName, STATIC_FLAGS);

            if (eventInfo == null)
            {
                return null;
            }

            if (eventInfo.EventHandlerType != typeof(Action<bool>))
            {
                return null;
            }
            
            var addMethod    = (Action<Action<bool>>)eventInfo.GetAddMethod(true).CreateDelegate(typeof(Action<Action<bool>>));
            var removeMethod = (Action<Action<bool>>)eventInfo.GetRemoveMethod(true).CreateDelegate(typeof(Action<Action<bool>>));

            return new ValidationEvent(addMethod, removeMethod);
        }
    }
}