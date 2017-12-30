﻿using GeekMDSuite.WebAPI.Core.DataAccess.Repositories;
using GeekMDSuite.WebAPI.DataAccess.Context;
using GeekMDSuite.WebAPI.Presentation.EntityModels;

namespace GeekMDSuite.WebAPI.DataAccess.Repositories
{
    public class AudiogramRepository : Repository<AudiogramEntity>, IAudiogramRepository
    {
        public AudiogramRepository(GeekMdSuiteDbContext context) : base (context)
        {
        }
    }
}