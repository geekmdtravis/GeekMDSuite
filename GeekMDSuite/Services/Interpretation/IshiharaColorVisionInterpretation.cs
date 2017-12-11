﻿using System;
using System.Collections.Generic;
using System.Linq;
using GeekMDSuite.Procedures;

// Ishihara Test
// 06 plate interpretation: 6/6 to pass
// 10 plate interpretation: looking for details??? Not implemnnted
// 14 Plate interpretation: 10/11 of plates 1-11: https://web.stanford.edu/group/vista/wikiupload/0/0a/Ishihara.14.Plate.Instructions.pdf
// 24 Plate interpretation: 13/15 of plates 1-15: http://www.dfis.ubi.pt/~hgil/P.V.2/Ishihara/Ishihara.24.Plate.TEST.Book.pdf
// 38 Plate interpretation: 17/21 of plates 1-21: http://zonemedical.com.au/c.728341/site/PDF/PAL/Ishihara-Test-User-Manual.pdf

namespace GeekMDSuite.Services.Interpretation
{
    public abstract class IshiharaColorVisionInterpretation
    {
        protected IshiharaColorVisionInterpretation(List<IshiharaPlateAnswer> answerList, IshiharaTestType testType)
        {
            TestType = testType;
            AnswerList = answerList ?? throw new ArgumentNullException(nameof(answerList));
        }
        
        public IshiharaTestType TestType { get; }
        public List<IshiharaPlateAnswer> AnswerList { get; }
        
        protected abstract List<IshiharaPlate> PlateSet { get; set; }

        protected abstract IshiharaResultFlag Classify();

        protected IshiharaResultFlag AssessIshiharaVisionAssessment()
        {
            if (TestType == IshiharaTestType.Ishihara6)
                return EvaluatePlateAnswers(new IshiharaPlateReadLimits(IshiharaTestType.Ishihara6));
            if (TestType == IshiharaTestType.Ishihara10)
                throw new NotImplementedException();
            if (TestType == IshiharaTestType.Ishihara14)
                return EvaluatePlateAnswers(new IshiharaPlateReadLimits(IshiharaTestType.Ishihara14));
            return EvaluatePlateAnswers(TestType == IshiharaTestType.Ishihara24 
                ? new IshiharaPlateReadLimits(IshiharaTestType.Ishihara24) 
                : new IshiharaPlateReadLimits(IshiharaTestType.Ishihara38));
            
        }

        private IshiharaResultFlag EvaluatePlateAnswers(IshiharaPlateReadLimits limits)
        {
            var count = AnswerList.Count(CompareNormalPlateReadCountToLimits(limits));
            if (count >= limits.LowerLimitOfPass) return IshiharaResultFlag.NormalVision;
            return count <= limits.UpperLimitOfFail ? IshiharaResultFlag.ColorVisionDeficit : IshiharaResultFlag.IndeterminantResult;
        }

        private static Func<IshiharaPlateAnswer, bool> CompareNormalPlateReadCountToLimits(IshiharaPlateReadLimits ishiharaPlateReadLimits)
        {
            return answer => answer.PlateRead == IshiharaAnswerResult.NormalVision && answer.PlateNumber <= ishiharaPlateReadLimits.SlidesToEvaluate;
        }
        
        private class IshiharaPlateReadLimits
        {
            public IshiharaPlateReadLimits(IshiharaTestType testType)
            {
                UpperLimitOfFail = DetermineUpperLimitOfFail(testType);
                LowerLimitOfPass = DetermineLowerLimitOfPass(testType);
                SlidesToEvaluate = DetermineNumberOfSlidesToEvaluate(testType);
            }

            public int UpperLimitOfFail { get; }
            public int LowerLimitOfPass { get; }
            public int SlidesToEvaluate { get; }
        
            private static int DetermineNumberOfSlidesToEvaluate(IshiharaTestType testType)
            {
                if (testType == IshiharaTestType.Ishihara6) return 6;
                if (testType == IshiharaTestType.Ishihara10) return 10;
                if (testType == IshiharaTestType.Ishihara14) return 14;
                return testType == IshiharaTestType.Ishihara24 ? 24 : 38;
            }
            private static int DetermineUpperLimitOfFail(IshiharaTestType testType)
            {
                if (testType == IshiharaTestType.Ishihara6) return 5;
                if (testType == IshiharaTestType.Ishihara10) throw new NotImplementedException();
                if (testType == IshiharaTestType.Ishihara14) return 7;
                return testType == IshiharaTestType.Ishihara24 ? 9 : 13;
            }
            private static int DetermineLowerLimitOfPass(IshiharaTestType testType)
            {
                if (testType == IshiharaTestType.Ishihara6) return 6;
                if (testType == IshiharaTestType.Ishihara10) throw new NotImplementedException();
                if (testType == IshiharaTestType.Ishihara14) return 10;
                return testType == IshiharaTestType.Ishihara24 ? 13 : 17;
            }
        }
    }
}