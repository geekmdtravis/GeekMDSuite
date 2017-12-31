﻿namespace GeekMDSuite.WebAPI.Core.Models
{
    public interface IEntity<in TInherited> : IMapProperties<TInherited>
    {
        int Id { get; set; }
    }
}