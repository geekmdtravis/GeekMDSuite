﻿using System;
using System.ComponentModel;
using GeekMDSuite.Procedures;

namespace GeekMDSuite.Services.Interpretation
{
    public class FunctionalMovementScreenClassification
    {
        internal FunctionalMovementScreenClassification(
            FunctionalMovementScreen functionalMovementScreen)
        {
            DeepSquat = ClassifyMovement(functionalMovementScreen.DeepSquat);
            HurdleStep = ClassifyMovement(functionalMovementScreen.HurdleStep);
            ShoulderMobility = ClassifyMovement(functionalMovementScreen.ShoulderMobility);
            InlineLunge = ClassifyMovement(functionalMovementScreen.InlineLunge);
            ActiveStraightLegRaise = ClassifyMovement(functionalMovementScreen.ActiveStraightLegRaise);
            TrunkStabilityPushup = ClassifyMovement(functionalMovementScreen.TrunkStabilityPushup);
            RotaryStability = ClassifyMovement(functionalMovementScreen.RotaryStability);
            Score = GetTotalScore(functionalMovementScreen);
        }


        public FmsMovementClassification DeepSquat { get; }
        public FmsMovementClassification HurdleStep { get; }
        public FmsMovementClassification ShoulderMobility { get; }
        public FmsMovementClassification InlineLunge { get; }
        public FmsMovementClassification ActiveStraightLegRaise { get; }
        public FmsMovementClassification TrunkStabilityPushup { get; }
        public FmsMovementClassification RotaryStability { get; }
        public int Score { get; }

        private static int GetTotalScore(FunctionalMovementScreen functionalMovementScreen)
        {
            return functionalMovementScreen.ActiveStraightLegRaise.Score +
                   functionalMovementScreen.DeepSquat.Score +
                   functionalMovementScreen.HurdleStep.Score +
                   functionalMovementScreen.InlineLunge.Score +
                   functionalMovementScreen.RotaryStability.Score +
                   functionalMovementScreen.ShoulderMobility.Score +
                   functionalMovementScreen.TrunkStabilityPushup.Score;
        }
        
        private static FmsMovementClassification ClassifyMovement(FmsMovementSet movementSet)
        {
            return new FmsMovementClassification(
                MovementSetClassification.CalculateFlag(movementSet),
                MovementSetClassification.RecommendedAction(movementSet)
            );
        }
        
        private static FmsMovementClassification ClassifyMovement(FmsMovementData movementSet)
        {
            return new FmsMovementClassification(
                MovementClassification.CalculateFlag(movementSet),
                MovementClassification.RecommendedAction(movementSet)
            );
        }

        private static class MovementClassification
        {
            public static FmsScoreFlag CalculateFlag(FmsMovementData movement)
            {
                var score = movement.Score;
                
                if (score == 0)
                    return FmsScoreFlag.L0R0;
                if (score == 1)
                    return FmsScoreFlag.L1R1;
                if (score == 2)
                    return FmsScoreFlag.L2R2;
                if (score == 3)
                    return FmsScoreFlag.L3R3;
                throw new ArgumentOutOfRangeException(nameof(score));
            }

            public static FmsRecommendedAction RecommendedAction(FmsMovementData movement)
            {
                var flag = CalculateFlag(movement);
                
                if (flag == FmsScoreFlag.L0R0)
                    return FmsRecommendedAction.MedicalAttention;
                if (flag == FmsScoreFlag.L1R1)
                    return FmsRecommendedAction.Restricted;
                if (flag == FmsScoreFlag.L2R2 || flag == FmsScoreFlag.L3R3)
                    return FmsRecommendedAction.Unrestricted;
                throw new InvalidEnumArgumentException(nameof(flag));
            }
        }

        private static class MovementSetClassification
        {
            public static FmsScoreFlag CalculateFlag(FmsMovementSet set)
            {
                var left = set.Left;
                var right = set.Right;
                
                switch(left.Score) {
                    default:
                        switch(right.Score) {
                            default:
                                return FmsScoreFlag.L0R0;
                            case 1:
                                return FmsScoreFlag.L0R1;
                            case 2:
                                return FmsScoreFlag.L0R2;
                            case 3:
                                return FmsScoreFlag.L0R3;
                        }
                    case 1:
                        switch(right.Score) {
                            default:
                                return FmsScoreFlag.L1R0;
                            case 1:
                                return FmsScoreFlag.L1R1;
                            case 2:
                                return FmsScoreFlag.L1R2;
                            case 3:
                                return FmsScoreFlag.L1R3;
                        }
                    case 2:
                        switch(right.Score) {
                            default:
                                return FmsScoreFlag.L2R0;
                            case 1:
                                return FmsScoreFlag.L2R1;
                            case 2:
                                return FmsScoreFlag.L2R2;
                            case 3:
                                return FmsScoreFlag.L2R3;
                        }
                    case 3:
                        switch(right.Score) {
                            default:
                                return FmsScoreFlag.L3R0;
                            case 1:
                                return FmsScoreFlag.L3R1;
                            case 2:
                                return FmsScoreFlag.L3R2;
                            case 3:
                                return FmsScoreFlag.L3R3;
                    }
            }
        }
        // Recommended actions to take include laterality. The 
        // calculated score only contributes to the total score
        // and if the total score is below a threshold a flag is 
        // thrown. But, assymetry also has importance.
        public static FmsRecommendedAction RecommendedAction(FmsMovementSet fmsMovementSet)
        {
            var flag = CalculateFlag(fmsMovementSet);
            switch (flag) {
                // Zero scores anywhere require medical attention
                case FmsScoreFlag.L0R0:
                case FmsScoreFlag.L0R1:
                case FmsScoreFlag.L0R2:
                case FmsScoreFlag.L0R3:
                case FmsScoreFlag.L1R0:
                case FmsScoreFlag.L2R0:
                case FmsScoreFlag.L3R0:
                    return FmsRecommendedAction.MedicalAttention;
                // 1s or assymetry require caution (restricted)
                case FmsScoreFlag.L1R1:
                case FmsScoreFlag.L1R2:
                case FmsScoreFlag.L1R3:
                case FmsScoreFlag.L2R1:
                case FmsScoreFlag.L2R3:
                case FmsScoreFlag.L3R1:
                case FmsScoreFlag.L3R2:
                    return FmsRecommendedAction.Restricted;
                // Symmetric 2's and 3's do not require significant supervision.
                case FmsScoreFlag.L2R2:
                case FmsScoreFlag.L3R3:
                    return FmsRecommendedAction.Unrestricted;
                default:
                    throw new InvalidEnumArgumentException(nameof(flag));
                
            }
        }
        }
    }
}