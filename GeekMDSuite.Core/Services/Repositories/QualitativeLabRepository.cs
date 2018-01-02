﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeekMDSuite.Core.Analytics.Classification;
using GeekMDSuite.Core.Helpers;
using GeekMDSuite.Core.LaboratoryData;
using Newtonsoft.Json;

namespace GeekMDSuite.Core.Services.Repositories
{
    internal static class QualitativeLabRepository
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public static IEnumerable<QualitativeLabClassificationModel> GetAllLabs()
        {
            var jsonFile = Reflection.GetAssetFromExecutingAssembly("qualitative_labs.json");
            return JsonConvert.DeserializeObject<List<QualitativeLabClassificationModel>>(jsonFile);
        }

        public static QualitativeLabClassificationModel GetLab(IQualitativeLab lab) => 
            GetAllLabs().First(l => string.Equals(l.LabName.ToString(), lab.Type.ToString(), StringComparison.CurrentCultureIgnoreCase));
    }
}