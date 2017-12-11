﻿using System;
using System.Collections.Generic;
using GeekMDSuite.Procedures;
using GeekMDSuite.Services.Respositories;

namespace GeekMDSuite.Services.Interpretation
{
    public class IshiharaTenPlateInterpretation : IshiharaColorVisionInterpretation, IInterpretable<IshiharaResultFlag>
    {
        public IshiharaTenPlateInterpretation(List<IshiharaPlateAnswer> answerList) : base(answerList, IshiharaTestType.Ishihara10)
        {
            PlateSet = IshiharaPlateSetRepository.SixPlateScreen();
            if(answerList.Count != PlateSet.Count) 
                throw new Exception(nameof(answerList) + " item count doesn't match test.");
        }
        
        protected sealed override List<IshiharaPlate> PlateSet { get; set; }
        protected sealed override IshiharaResultFlag Classify() => AssessIshiharaVisionAssessment();

        public InterpretationText Interpretation => throw new NotImplementedException();
        public IshiharaResultFlag Classification => Classify();
    }
}