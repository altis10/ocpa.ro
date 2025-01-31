﻿using System;
using System.Globalization;
using System.IO;
using ThorusCommon.IO;

namespace Meteo.Helpers
{
    public static class DataHelper
    {
        static DataHelper()
        {
        }

        public static int GetCalendarRange(ref DateTime dtStart, ref DateTime dtEnd, int days)
        {
            string[] files = GetDataFiles("L_00_MAP*.thd", days);

            string fileTitle = Path.GetFileNameWithoutExtension(files[0]);
            string datePart = fileTitle.Substring(9, 10);

            dtStart = DateTime.ParseExact(datePart, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            fileTitle = Path.GetFileNameWithoutExtension(files[files.Length - 1]);
            datePart = fileTitle.Substring(9, 10);

            dtEnd = DateTime.ParseExact(datePart, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            return files.Length;
        }

        public static float GetDataPoint(string dataType, DateTime dt, int r, int c)
        {
            string dtStr = dt.ToString("yyyy-MM-dd");
            string fileName = $"{dataType}_MAP_{dtStr}_00.thd";
            string filePath = Path.Combine(AppFolders.DataFolder, fileName);

            //return new MatrixFile(filePath, true).Matrix[r, c];
            return DataReader.ReadFromFile(filePath, r, c);
        }

        public static string[] GetDataFiles(string mask, int count)
        {
            string[] files = Directory.GetFiles(AppFolders.DataFolder, mask);

            if (count < 1)
                return files;

            string[] ret = new string[count];

            Array.Copy(files, ret, count);

            return ret;

        }
    }
}