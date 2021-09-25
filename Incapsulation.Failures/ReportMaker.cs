using System;
using System.Collections.Generic;
using System.Linq;

namespace Incapsulation.Failures
{
    public enum FailureType
    {
        UnexpectedShutdown = 0,
        ShortNonResponding = 1,
        HardwareFailures = 2,
        ConnectionProblems = 3
    }

    public struct Device
    {
        public readonly int Id;
        public readonly string Name;

        public Device(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public struct Failure
    {
        public readonly FailureType Type;
        public readonly DateTime Date;

        public Failure(FailureType type, DateTime date)
        {
            Type = type;
            Date = date;
        }
    }

    public class Common
    {
        public static bool IsFailureSerious(FailureType failureType)
        {
            return failureType == FailureType.UnexpectedShutdown ||
                   failureType == FailureType.HardwareFailures;
        }
    }

    public class ReportMaker
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="failureTypes">
        ///     0 for unexpected shutdown,
        ///     1 for short non-responding,
        ///     2 for hardware failures,
        ///     3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceId,
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            var targetDate = new DateTime(year, month, day);
            var failureList = failureTypes
                .Select((type, index) => new Failure((FailureType) type, new DateTime((int) times[index][2], (int) times[index][1], (int) times[index][0]))).ToList();
            var deviceList = deviceId.Select((id, index) => new Device(id, devices[index]["Name"].ToString())).ToList();
            
            return FindDevicesFailedBeforeDate(targetDate, failureList, deviceList);
        }

        public static List<string> FindDevicesFailedBeforeDate(DateTime targetDate, List<Failure> failures, List<Device> devices)
        {
            var deviceNames = new List<string>();
            
            for (var i = 0; i < failures.Count; i++)
            {
                if (failures[i].Date < targetDate && Common.IsFailureSerious(failures[i].Type))
                {
                    deviceNames.Add(devices[i].Name);
                }
            }

            return deviceNames;
        }
    }
}