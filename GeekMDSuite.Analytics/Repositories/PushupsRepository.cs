﻿using System.Collections.Generic;
using System.Linq;
using GeekMDSuite.Core.Models;
using GeekMDSuite.Utilities.Generic;

namespace GeekMDSuite.Analytics.Repositories
{
    internal static class PushupsRepository
    {
        public static MuscularStrengthRepositoryEntry GetRanges(Patient patient)
        {
            return Gender.IsGenotypeXy(patient.Gender)
                ? Male.List.First(entry => entry.AgeRange.ContainsClosed(patient.Age))
                : Female.List.First(entry => entry.AgeRange.ContainsClosed(patient.Age));
        }

        private static class Male
        {
            public static readonly List<MuscularStrengthRepositoryEntry> List =
                new List<MuscularStrengthRepositoryEntry>
                {
                    GetMaleAgeLessThanMax(),
                    GetMaleAgeLessThan60(),
                    GetMaleAgeLessThan50(),
                    GetMaleAgeLessThan40(),
                    GetMaleAgeLessThan30(),
                    GetMaleAgeLessThan20()
                };

            private static MuscularStrengthRepositoryEntry GetMaleAgeLessThan20()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(0, 19), 4, 11, 19, 35, 47, 56);
            }

            private static MuscularStrengthRepositoryEntry GetMaleAgeLessThan30()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(20, 29), 4, 10, 17, 30, 39, 47);
            }

            private static MuscularStrengthRepositoryEntry GetMaleAgeLessThan40()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(30, 39), 2, 8, 13, 25, 34, 41);
            }

            private static MuscularStrengthRepositoryEntry GetMaleAgeLessThan50()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(40, 49), 1, 6, 11, 21, 28, 34);
            }

            private static MuscularStrengthRepositoryEntry GetMaleAgeLessThan60()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(50, 59), 1, 5, 9, 18, 25, 31);
            }

            private static MuscularStrengthRepositoryEntry GetMaleAgeLessThanMax()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(60, int.MaxValue), 1, 3, 6, 17, 24, 30);
            }
        }

        private static class Female
        {
            public static readonly List<MuscularStrengthRepositoryEntry> List =
                new List<MuscularStrengthRepositoryEntry>
                {
                    GetFemaleAgeLessThanMax(),
                    GetFemaleAgeLessThan60(),
                    GetFemaleAgeLessThan50(),
                    GetFemaleAgeLessThan40(),
                    GetFemaleAgeLessThan30(),
                    GetFemaleAgeLessThan20()
                };

            private static MuscularStrengthRepositoryEntry GetFemaleAgeLessThan20()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(0, 19), 2, 6, 11, 21, 27, 35);
            }

            private static MuscularStrengthRepositoryEntry GetFemaleAgeLessThan30()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(20, 29), 2, 7, 12, 23, 30, 36);
            }

            private static MuscularStrengthRepositoryEntry GetFemaleAgeLessThan40()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(30, 39), 1, 5, 10, 22, 30, 37);
            }

            private static MuscularStrengthRepositoryEntry GetFemaleAgeLessThan50()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(40, 49), 1, 4, 8, 18, 25, 31);
            }

            private static MuscularStrengthRepositoryEntry GetFemaleAgeLessThan60()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(50, 59), 1, 3, 7, 15, 21, 25);
            }

            private static MuscularStrengthRepositoryEntry GetFemaleAgeLessThanMax()
            {
                return new MuscularStrengthRepositoryEntry(new Interval<int>(60, int.MaxValue), 1, 2, 5, 13, 19, 23);
            }
        }
    }
}