﻿namespace GeekMDSuite.WebAPI.Models
{
    public interface IMapProperties<in T>
    {
        void MapValues(T subject);
    }
}