using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ReservationSystem.Shared.Utilities
{
    public class Result<T> : Result
    {
        public Result(T value)
        {
            Value = value;
        }

#pragma warning disable 8618
        public Result()
#pragma warning restore 8618
        {
            
        }

        public T Value { get; }
    }

    public class Result
    {
        public Result()
        {
            
        }
        
        public bool IsSuccess => Errors.Count == 0;

        public Dictionary<string, string> Errors { get; set; } = new();

        public void AddError(string propertyName, string error)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new InvalidOperationException("Property name can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                throw new InvalidOperationException("Error message can not be empty.");
            }
            
            Errors.Add(propertyName, error);
        }
        
    }
}