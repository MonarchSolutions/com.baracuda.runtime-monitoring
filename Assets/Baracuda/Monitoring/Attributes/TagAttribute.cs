﻿using System;

namespace Baracuda.Monitoring.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Event)]
    public sealed class TagAttribute : Attribute
    {
        public readonly string[] Tags;

        public TagAttribute(string tag)
        {
            Tags = new[] {tag};
        }
        
        public TagAttribute(params string[] tags)
        {
            Tags = tags;
        }
    }
}