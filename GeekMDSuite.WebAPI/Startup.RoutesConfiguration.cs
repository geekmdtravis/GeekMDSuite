﻿using AspNet.Mvc.TypedRouting.Routing;
using GeekMDSuite.Analytics.Classification;
using GeekMDSuite.Core.Models;
using GeekMDSuite.Core.Models.Procedures;
using GeekMDSuite.WebAPI.Core.DataAccess.Repositories.Filters;
using GeekMDSuite.WebAPI.Presentation.Controllers;
using GeekMDSuite.WebAPI.Presentation.Controllers.AnalyticsControllers;
using GeekMDSuite.WebAPI.Presentation.Controllers.AnalyticsControllers.CompositeScores;
using GeekMDSuite.WebAPI.Presentation.Controllers.AnalyzablePatientDataControllers;
using Microsoft.AspNetCore.Builder;
using System;
using GeekMDSuite.WebAPI.Presentation.Controllers.PatientController;
using GeekMDSuite.WebAPI.Presentation.StubFromUserModels;

namespace GeekMDSuite.WebAPI
{
    public partial class Startup
    {
        private Action<ITypedRouteBuilder> RoutesConfiguration()
        {
            var baseUrl = Configuration.GetSection("ApiBaseUrl").Value;

            return routes =>
            {
                ConfigurePatientControllerRoutes(baseUrl, routes);
                ConfigureVisitControllerRoutes(baseUrl, routes);
                ConfigureDataRoutes(baseUrl, routes);
                ConfigureAnalyticsRoutes(baseUrl, routes);
            };
        }

        private static void ConfigurePatientControllerRoutes(string baseUrl,
            ITypedRouteBuilder routes)
        {
            routes.Add(baseUrl + "patient/",
                route => route.ToController<PatientController>());
            routes.Get("",
                route => route.ToAction<PatientController>(a => a.GetBySearch(With.Any<PatientDataSearchFilter>())));
            routes.Get("{guid}/visits",
                route => route.ToAction<PatientController>(a => a.GetVisits(With.Any<Guid>())));
            routes.Get("{guid}",
                route => route.ToAction<PatientController>(a => a.GetByGuid(With.Any<Guid>())));
        }

        private static void ConfigureVisitControllerRoutes(string baseUrl,
            ITypedRouteBuilder routes)
        {
            routes.Add(baseUrl + "visit/",
                route => route.ToController<VisitController>());
            routes.Get("",
                route => route.ToAction<VisitController>(a => a.GetBySearch(With.Any<VisitDataSearchFilter>())));
            routes.Get("{guid}",
                route => route.ToAction<VisitController>(a => a.GetByGuid(With.Any<Guid>())));
        }

        private static void ConfigureAnalyticsRoutes(string baseUrl, ITypedRouteBuilder routes)
        {
            var classifyUri = baseUrl + "classify/";
            routes.Add(classifyUri, route => route.ToController<ClassifyController>());
            routes.Post("audiogram/",
                route => route.ToAction<ClassifyController>(a => a.PostToAudiogram(With.Any<Audiogram>())));
            routes.Post("bp/",
                route => route.ToAction<ClassifyController>(a => a.PostToBloodPressure(With.Any<BloodPressure>())));
            routes.Post("bodycomp/",
                route => route.ToAction<ClassifyController>(a =>
                    a.PostToBodyComposition(With.Any<BodyCompositionClassificationParameters>())));
            routes.Post("bodycompexp/",
                route => route.ToAction<ClassifyController>(a =>
                    a.PostToBodyCompositionExpanded(With.Any<BodyCompositionExpandedClassificationParameters>())));

            var compositeScoresUri = classifyUri + "composite/";
            routes.Add(compositeScoresUri, route => route.ToController<CompositeScoresController>());
        }

        private static void ConfigureDataRoutes(string baseUrl, ITypedRouteBuilder routes)
        {
            var dataUrl = baseUrl + "data/";
            
            routes.Add(dataUrl + "audiogram/", route => route.ToController<AudiogramsController>());
            routes.Get("", route => route.ToAction<AudiogramsController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<AudiogramsController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<AudiogramsController>(a => a.Post(With.Any<AudiogramStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<AudiogramsController>(a => a.Put(With.Any<int>(), With.Any<AudiogramStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<AudiogramsController>(a => a.Delete(With.Any<int>())));
            
            routes.Add(dataUrl + "bp/", route => route.ToController<BloodPressuresController>());
            routes.Get("", route => route.ToAction<BloodPressuresController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<BloodPressuresController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<BloodPressuresController>(a => a.Post(With.Any<BloodPressureStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<BloodPressuresController>(a => a.Put(With.Any<int>(), With.Any<BloodPressureStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<BloodPressuresController>(a => a.Delete(With.Any<int>())));
            
            routes.Add(dataUrl + "carotidus/", route => route.ToController<CarotidUltrasoundsController>());
            routes.Get("", route => route.ToAction<CarotidUltrasoundsController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<CarotidUltrasoundsController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<CarotidUltrasoundsController>(a => a.Post(With.Any<CarotidUltrasoundStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<CarotidUltrasoundsController>(a => a.Put(With.Any<int>(), With.Any<CarotidUltrasoundStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<CarotidUltrasoundsController>(a => a.Delete(With.Any<int>())));
            
            routes.Add(dataUrl + "centralbp/", route => route.ToController<CentralBloodPressuresController>());
            routes.Get("", route => route.ToAction<CentralBloodPressuresController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<CentralBloodPressuresController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<CentralBloodPressuresController>(a => a.Post(With.Any<CentralBloodPressureStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<CentralBloodPressuresController>(a => a.Put(With.Any<int>(), With.Any<CentralBloodPressureStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<CentralBloodPressuresController>(a => a.Delete(With.Any<int>())));
            
            routes.Add(dataUrl + "fms/", route => route.ToController<FunctionalMovementScreensController>());
            routes.Get("", route => route.ToAction<FunctionalMovementScreensController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<FunctionalMovementScreensController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<FunctionalMovementScreensController>(a => a.Post(With.Any<FunctionalMovementScreenStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<FunctionalMovementScreensController>(a => a.Put(With.Any<int>(), With.Any<FunctionalMovementScreenStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<FunctionalMovementScreensController>(a => a.Delete(With.Any<int>())));
            
            routes.Add(dataUrl + "gripstrength/", route => route.ToController<GripStrengthsController>());
            routes.Get("", route => route.ToAction<GripStrengthsController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<GripStrengthsController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<GripStrengthsController>(a => a.Post(With.Any<GripStrengthStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<GripStrengthsController>(a => a.Put(With.Any<int>(), With.Any<GripStrengthStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<GripStrengthsController>(a => a.Delete(With.Any<int>())));
            
            routes.Add(dataUrl + "ishihara/", route => route.ToController<IshiharaSixPlateScreensController>());
            routes.Get("", route => route.ToAction<IshiharaSixPlateScreensController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<IshiharaSixPlateScreensController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<IshiharaSixPlateScreensController>(a => a.Post(With.Any<IshiharaSixPlateStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<IshiharaSixPlateScreensController>(a => a.Put(With.Any<int>(), With.Any<IshiharaSixPlateStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<IshiharaSixPlateScreensController>(a => a.Delete(With.Any<int>())));
            
            routes.Add(dataUrl + "occularpressures/", route => route.ToController<OccularPressuresController>());
            routes.Get("", route => route.ToAction<OccularPressuresController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<OccularPressuresController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<OccularPressuresController>(a => a.Post(With.Any<OccularPressureStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<OccularPressuresController>(a => a.Put(With.Any<int>(), With.Any<OccularPressureStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<OccularPressuresController>(a => a.Delete(With.Any<int>())));
            
            routes.Add(dataUrl + "peripheralvisions/", route => route.ToController<PeripheralVisionsController>());
            routes.Get("", route => route.ToAction<PeripheralVisionsController>(a => a.GetBySearch(With.Any<EntityDataFindFilter>())));
            routes.Get("{id}", route => route.ToAction<PeripheralVisionsController>(a => a.GetById(With.Any<int>())));
            routes.Post("", route => route.ToAction<PeripheralVisionsController>(a => a.Post(With.Any<PeripheralVisionStubFromUser>())));
            routes.Put("{id}", route => route.ToAction<PeripheralVisionsController>(a => a.Put(With.Any<int>(), With.Any<PeripheralVisionStubFromUser>())));
            routes.Delete("{id}", route => route.ToAction<PeripheralVisionsController>(a => a.Delete(With.Any<int>())));
            
//            routes.Add(dataUrl + "bodycomp/", route => route.ToController<BodyCompositionController>());
//            routes.Add(dataUrl + "bodycompexp/", route => route.ToController<BodyCompositionExpandedController>());
//            routes.Add(dataUrl + "pushups/", route => route.ToController<PushupController>());
//            routes.Add(dataUrl + "sitandreach/", route => route.ToController<SitAndReachController>());
//            routes.Add(dataUrl + "situps/", route => route.ToController<SitupsController>());
//            routes.Add(dataUrl + "spirometry/", route => route.ToController<SpirometryController>());
//            routes.Add(dataUrl + "treadmill/", route => route.ToController<TreadmillExerciseStressTestController>());
//            routes.Add(dataUrl + "visualacuity/", route => route.ToController<VisualAcuityController>());
//            routes.Add(dataUrl + "vitals/", route => route.ToController<VitalSignsController>());
//
//            var patientActivitiesUri = dataUrl + "activities/";
//            routes.Add(patientActivitiesUri + "resistance", route => route.ToController<ResistanceRegimenController>());
//            routes.Add(patientActivitiesUri + "cardio", route => route.ToController<CardiovascularRegimenController>());
//            routes.Add(patientActivitiesUri + "stretching", route => route.ToController<StretchingRegimenController>());
        }
    }
}