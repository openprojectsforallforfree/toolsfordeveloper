using System;
using System.Collections.Generic;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Picasso.Common
{
    //For school software
    //Store key in a text file in build location
    //generate from machine
    //save
    //limit marks to 5 students
    public class LockSchoolMis
    {
        public static string GetKeyCode(string lockcode)
        {
            return GenerateCodes(lockcode + "schoolMIS2015", 10);
        }

        public static string GetLockCode()
        {
            var divices = Getinfo("Win32_DiskDrive");
            for (int i = 0; i <= divices.Count - 1; i++)
            {
                System.Collections.Generic.Dictionary<string, string> values = new System.Collections.Generic.Dictionary<string, string>();
                values = (System.Collections.Generic.Dictionary<string, string>)divices[i];
                if (values["InterfaceType"].ToUpper() == "IDE")
                {
                    string _DiskSerial = Buildstring(values);
                    var temp = _DiskSerial + "marksheetkey2014";
                    temp = GenerateCodes(temp,10);
                    return temp;
                }
            }
            throw new Exception("Information Not available to register");
        }

        #region Private Members
       

        private static System.Collections.ArrayList Getinfo(string Key)
        {
            System.Collections.ArrayList devices = new System.Collections.ArrayList();
            string name = "";
            string lst = "";
            //'Win32_DiskDrive
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + Key);
            try
            {
                foreach (ManagementObject share in searcher.Get())
                {
                    System.Collections.Generic.Dictionary<string, string> values = new System.Collections.Generic.Dictionary<string, string>();
                    try
                    {
                        lst = share["Name"].ToString();
                        values.Add("drivekonam", lst);
                    }
                    catch
                    {
                        lst = share.ToString();
                        values.Add("drivekonam", lst);
                    }

                    if (share.Properties.Count <= 0)
                    {
                        return null;
                    }

                    foreach (PropertyData PC in share.Properties)
                    {
                        name = PC.Name;

                        if (PC.Value != null && !string.IsNullOrEmpty(PC.Value.ToString()))
                        {
                            switch (PC.Value.GetType().ToString())
                            {
                                case "System.String[]":
                                    string[] str = (string[])PC.Value;

                                    string str2 = "";
                                    foreach (string st in str)
                                    {
                                        str2 += st + " ";
                                    }

                                    lst = str2;

                                    break;

                                case "System.UInt16[]":
                                    ushort[] shortData = (ushort[])PC.Value;

                                    string tstr2 = "";
                                    foreach (ushort st in shortData)
                                    {
                                        tstr2 += st.ToString() + " ";
                                    }

                                    lst = tstr2;

                                    break;

                                default:

                                    lst = PC.Value.ToString();
                                    break;
                            }
                            values.Add(name, lst);
                        }
                    }
                    devices.Add(values);
                }
            }
            catch
            {
            }
            return devices;
        }

        private static string Buildstring(System.Collections.Generic.Dictionary<string, string> values)
        {
            string a = "";
            try
            {
                a += values["Caption"];
                a += values["BytesPerSector"];
                a += values["Manufacturer"];
                a += values["PNPDeviceID"];
                a += values["Model"];
                a += values["Size"];
                a += values["SectorsPerTrack"];
                a += values["TotalCylinders"];
                a += values["TotalHeads"];
                a += values["TotalSectors"];
                a += values["TotalTracks"];
                a += values["TracksPerCylinder"];
            }
            catch
            {
                return "Not registrable";
            }

            //drivekonam
            //BytesPerSector
            //Capabilities
            //CapabilityDescriptions
            //Caption
            //ConfigManagerErrorCode
            //ConfigManagerUserConfig
            //CreationClassName
            //Description
            //DeviceID
            //FirmwareRevision
            //Index
            //InterfaceType
            //Manufacturer
            //MediaLoaded
            //MediaType
            //Model
            //Name
            //Partitions
            //PNPDeviceID
            //SCSIBus
            //SCSILogicalUnit
            //SCSIPort
            //SCSITargetId
            //SectorsPerTrack
            //SerialNumber
            //Signature
            //Size
            //Status
            //SystemCreationClassName
            //SystemName
            //TotalCylinders
            //TotalHeads
            //TotalSectors
            //TotalTracks
            //TracksPerCylinder

            return a;
        }

        public  static string GenerateCodes(string temp,int size)
        {
            temp = temp.Replace(" ", "");//than a
            //generate Reference based on
            //appname,password,and harddisk serial
            //generate serial key based on Reference
            HashAlgorithm Hash = default(HashAlgorithm);
            byte[] HashBytes = null;
            //string tem = null;
            UnicodeEncoding UNIEncoding = new UnicodeEncoding();

            string _CustRef = null;

            Hash = new MD5CryptoServiceProvider();
            HashBytes = Hash.ComputeHash(UNIEncoding.GetBytes(temp));

            _CustRef = System.Convert.ToBase64String(HashBytes);
            //if longer than 20 chars, trim it to 20 chars
            if (_CustRef.Trim().Length > size) _CustRef = _CustRef.Substring(0, size);

            //convert it to upper case
            _CustRef = _CustRef.ToUpper();

            return _CustRef;
        }

        #endregion
    }

    //frm register => set key ,check key
    //orgdegail => check key if not set org name = > test company
    //Other => check key + count
    public class skgen
    {
        //all private

        #region Hrwr

        /// <summary>
        /// For each machine a different key is generated
        ///
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetKeyMachine()
        {
            System.Collections.ArrayList divices = new System.Collections.ArrayList();
            string temp = null;
            divices = getinfo("Win32_DiskDrive");
            string val = null;
            val = "IDE";
            for (int i = 0; i <= divices.Count - 1; i++)
            {
                System.Collections.Generic.Dictionary<string, string> values = new System.Collections.Generic.Dictionary<string, string>();
                values = (System.Collections.Generic.Dictionary<string, string>)divices[i];
                if (val == values["InterfaceType"].ToUpper())
                {
                    string _DiskSerial = buildstring(values);
                    temp = _DiskSerial + "justakey2013";
                    temp = GenerateCodes(temp);
                    return temp;
                }
            }
            throw new Exception("Information Not available to register");
        }

        public static string GetBuildDetail()
        {
            System.Collections.ArrayList divices = new System.Collections.ArrayList();
            StringBuilder sb = new StringBuilder();
            divices = getinfo("Win32_DiskDrive");
            string val = null;
            val = "IDE";
            for (int i = 0; i <= divices.Count - 1; i++)
            {
                System.Collections.Generic.Dictionary<string, string> values = new System.Collections.Generic.Dictionary<string, string>();
                values = (System.Collections.Generic.Dictionary<string, string>)divices[i];
                if (val == values["InterfaceType"].ToUpper())
                {
                    foreach (var item in values)
                    {
                        sb.AppendLine(item.Key + " : " + item.Value);
                    }
                }
            }
            return sb.ToString();
        }

        private static string buildstring(System.Collections.Generic.Dictionary<string, string> values)
        {
            string a = "";
            try
            {
                a += values["Caption"];
                a += values["BytesPerSector"];
                a += values["Manufacturer"];
                a += values["PNPDeviceID"];
                a += values["Model"];
                a += values["Size"];
                a += values["SectorsPerTrack"];
                a += values["TotalCylinders"];
                a += values["TotalHeads"];
                a += values["TotalSectors"];
                a += values["TotalTracks"];
                a += values["TracksPerCylinder"];
            }
            catch
            {
                return "Not registrable";
            }

            //drivekonam
            //BytesPerSector
            //Capabilities
            //CapabilityDescriptions
            //Caption
            //ConfigManagerErrorCode
            //ConfigManagerUserConfig
            //CreationClassName
            //Description
            //DeviceID
            //FirmwareRevision
            //Index
            //InterfaceType
            //Manufacturer
            //MediaLoaded
            //MediaType
            //Model
            //Name
            //Partitions
            //PNPDeviceID
            //SCSIBus
            //SCSILogicalUnit
            //SCSIPort
            //SCSITargetId
            //SectorsPerTrack
            //SerialNumber
            //Signature
            //Size
            //Status
            //SystemCreationClassName
            //SystemName
            //TotalCylinders
            //TotalHeads
            //TotalSectors
            //TotalTracks
            //TracksPerCylinder

            return a;
        }

        private static System.Collections.ArrayList getinfo(string Key)
        {
            System.Collections.ArrayList devices = new System.Collections.ArrayList();

            string name = "";
            string lst = "";
            //'Win32_DiskDrive
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from " + Key);
            try
            {
                foreach (ManagementObject share in searcher.Get())
                {
                    System.Collections.Generic.Dictionary<string, string> values = new System.Collections.Generic.Dictionary<string, string>();

                    // ListViewGroup grp;
                    //Region "drive name"
                    try
                    {
                        lst = share["Name"].ToString();
                        values.Add("drivekonam", lst);
                    }
                    catch
                    {
                        lst = share.ToString();
                        values.Add("drivekonam", lst);
                    }

                    if (share.Properties.Count <= 0)
                    {
                        // MessageBox.Show("No Information Available", "No Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return null;
                    }
                    //End Region

                    foreach (PropertyData PC in share.Properties)
                    {
                        name = PC.Name;
                        //Region "if"

                        if (PC.Value != null && !string.IsNullOrEmpty(PC.Value.ToString()))
                        {
                            switch (PC.Value.GetType().ToString())
                            {
                                case "System.String[]":
                                    string[] str = (string[])PC.Value;

                                    string str2 = "";
                                    foreach (string st in str)
                                    {
                                        str2 += st + " ";
                                    }

                                    lst = str2;

                                    break;

                                case "System.UInt16[]":
                                    ushort[] shortData = (ushort[])PC.Value;

                                    string tstr2 = "";
                                    foreach (ushort st in shortData)
                                    {
                                        tstr2 += st.ToString() + " ";
                                    }

                                    lst = tstr2;

                                    break;

                                default:

                                    lst = PC.Value.ToString();
                                    break;
                            }
                            values.Add(name, lst);
                        }
                        //End Region
                    }
                    devices.Add(values);
                }
            }
            catch
            {
            }

            // MessageBox.Show("can't get data because of the followeing error \n" + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return devices;
        }

        /// <summary>
        /// Generate a unique no for each usb drive and put ke in code itself then compile exe then distribute.
        /// Change later


        #endregion Hrwr

        //enctr
        public static string GenerateCodes(string temp)
        {
            temp = temp.Replace(" ", "");//than a
            //generate Reference based on
            //appname,password,and harddisk serial
            //generate serial key based on Reference
            HashAlgorithm Hash = default(HashAlgorithm);
            byte[] HashBytes = null;
            //string tem = null;
            UnicodeEncoding UNIEncoding = new UnicodeEncoding();

            string _CustRef = null;
            //Select Case _HashAlgorithm
            //    Case HashAlgorithms.MD5
            //        Hash = New MD5CryptoServiceProvider
            //    Case HashAlgorithms.SHA1
            //        Hash = New SHA1CryptoServiceProvider
            //    Case HashAlgorithms.SHA256
            //        Hash = New SHA256Managed
            //    Case HashAlgorithms.SHA384
            //        Hash = New SHA384Managed
            //    Case HashAlgorithms.SHA512
            //        Hash = New SHA512Managed
            //    Case Else 'default hash algorithm
            //        Hash = New MD5CryptoServiceProvider
            //End Select
            Hash = new MD5CryptoServiceProvider();
            HashBytes = Hash.ComputeHash(UNIEncoding.GetBytes(temp));

            _CustRef = System.Convert.ToBase64String(HashBytes);
            //if longer than 20 chars, trim it to 20 chars
            if (_CustRef.Trim().Length > 20) _CustRef = _CustRef.Substring(0, 20);

            //convert it to upper case
            _CustRef = _CustRef.ToUpper();

            return _CustRef;
        }

        #region Helper

        //Idea is to store the build dettail (hwl code etc for the machine if registered successfully
        public static string GetGegLockMachine(string inkey)
        {
            string reglc = inkey + " " + GetKeyMachine();
            return reglc;
        }

        #endregion Helper
    }


}