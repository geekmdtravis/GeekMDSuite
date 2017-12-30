﻿using System;

namespace GeekMDSuite.WebAPI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IPatientsRepository Patients { get; }
        IAudiogramRepository Audiograms { get; }
        ICarotidUltrasoundsRepository CarotidUltrasounds { get; }
        void Complete();
    }
}