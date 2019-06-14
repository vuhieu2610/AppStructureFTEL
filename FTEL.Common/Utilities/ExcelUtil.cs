using FTEL.Common.SqlService;
using FTEL.Common;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace FTEL.Common.Utilities
{
    public static class ExcelUtil
    {
        #region public method
        /// <summary>
        /// Đọc file excel
        /// </summary>
        /// <typeparam name="T">Object cần set dữ liệu</typeparam>
        /// <param name="pathFileExcel">Đường dẫn file excel</param>
        /// <param name="rowStart">Dòng bắt đầu dữ liệu</param>
        /// <param name="sheetName">Tên sheet</param>
        /// <returns>List object</returns>
        /// <author>luannv6 - 04-07-2018</author>
        public static ResultInfor<T> ReadFileExcelByStream<T>(Stream excelFileStream, string excelFielExtention, int rowStart, string sheetName) where T : new()
        {
            var rt = new ResultInfor<T>();

            try
            {
                if (excelFielExtention.ToLower() == ".xls")
                {
                    rt = ReadFileExcel2003<T>(excelFileStream, rowStart, sheetName);
                }
                else
                {
                    rt = ReadFileExcel2007<T>(excelFileStream, rowStart, sheetName);
                }
                if (!rt.HasListData)
                {
                    rt.rtResult = false;
                    rt.AddMSG(Environment.NewLine + "Không có bản ghi nào trong file excel.");
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(Libs.SerializeObject(new
                {
                    excelFileStream = excelFileStream,
                    excelFielExtention = excelFielExtention,
                    rowStart = rowStart,
                    sheetName = sheetName
                }), ex);
                rt.rtResult = false;
                rt.vListMSG.Append("Lỗi đọc file excel.");
            }

            return rt;
        }
        /// <summary>
        /// Đọc file excel
        /// </summary>
        /// <typeparam name="T">Object cần set dữ liệu</typeparam>
        /// <param name="nameFileExcel">Tên file excel</param>
        /// <param name="rowStart">Dòng bắt đầu dữ liệu</param>
        /// <param name="sheetName">Tên sheet</param>
        /// <returns>List object</returns>
        /// <author>luannv6 - 04-07-2018</author>
        public static ResultInfor<T> ReadFileExcel<T>(Stream excelFileStream, string excelFileExtention, int rowStart, string sheetName) where T : new()
        {
            var rt = new ResultInfor<T>();
            //string fullPath = HttpRuntime.AppDomainAppPath + ConfigUtil.FileDirectory + "/" + nameFileExcel;

            try
            {
                if (Path.GetExtension(excelFileExtention).ToLower() == ".xls")
                {
                    rt = ReadFileExcel2003<T>(excelFileStream, rowStart, sheetName);
                }
                else
                {
                    rt = ReadFileExcel2007<T>(excelFileStream, rowStart, sheetName);
                }
                if (!rt.HasListData)
                {
                    rt.rtResult = false;
                    rt.AddMSG(Environment.NewLine + "Không có bản ghi nào trong file excel.");
                }
            }
            catch (Exception)
            {
                rt.rtResult = false;
                rt.vListMSG.Append("Lỗi đọc file excel.");
            }

            return rt;
        }

        public static ResultInfor<T> ExportExcelFromList<T>(string fileName, string templateFileName, int rowStart, List<T> lstData) where T : new()
        {
            try
            {
                //var rt = ExcelUtil.ResponeExcelFromList<T>(fileName, templateFileName, rowStart, lstData);
                string handle = Guid.NewGuid().ToString();

                //RedisCacheProvider.GetRedis().Insert(handle, rt);

                fileName = Libs.CleanUrl(fileName);
                string fullPath = CreateFileExcel(fileName, HttpRuntime.AppDomainAppPath + ConfigUtil.tmp_ExportTemp + templateFileName, rowStart, lstData);
                if (!string.IsNullOrEmpty(fullPath))
                {
                    fullPath = fullPath.Replace(HttpRuntime.AppDomainAppPath + ConfigUtil.FileDirectory, "");
                }
                else
                {
                    fullPath = templateFileName;
                }
                //Luannv6 sửa lại luôn truyền về filepath kể cả không có dữ liệu
                if (lstData == null || lstData.Count == 0)
                {
                    return new ResultInfor<T>()
                    {
                        ERR_CODE = fullPath,
                        rtResult = false,
                        AddMSG2 = "Không có dữ liệu"
                    };
                }
                return new ResultInfor<T>()
                {
                    ERR_CODE = handle,
                    rtResult = true,
                    AddMSG2 = "Xuất excel thành công."
                };
            }
            catch (Exception ex)
            {
                return new ResultInfor<T>(false, "Lỗi xuất excel." + ex.ToString());
            }
        }

        //public async static Task<ResultInfor<T>> ExportExcelFromListAsync<T>(string fileName, string templateFileName, int rowStart, List<T> lstData) where T : new()
        //{
        //    try
        //    {
        //        var rt = ExcelUtil.ResponeExcelFromList<T>(fileName, templateFileName, rowStart, lstData);
        //        string handle = Guid.NewGuid().ToString();

        //        var result = await RedisCacheProvider.GetRedis().InsertAsync(handle, rt);

        //        return new ResultInfor<T>()
        //        {
        //            ERR_CODE = handle,
        //            rtResult = result,
        //            AddMSG2 = (result == true ? "Xuất excel thành công." : "Xuất excel không thành công.")
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultInfor<T>(false, "Lỗi xuất excel." + ex.ToString());
        //    }
        //}

        public static ResultInfor<T> ExportExcelFromList<T>(string fileName, string templateFileName, int rowStart, ResultInfor<T> in_item) where T : new()
        {
            try
            {
                var rt = ExcelUtil.ResponeExcelFromList<T>(fileName, templateFileName, rowStart, in_item.ListData);
                string handle = Guid.NewGuid().ToString();

                //RedisCacheProvider.GetRedis().Insert(handle, rt);

                fileName = Libs.CleanUrl(fileName);
                string fullPath = CreateFileExcel(fileName, HttpRuntime.AppDomainAppPath + ConfigUtil.tmp_ExportTemp + templateFileName, rowStart, in_item.ListData);
                fullPath = fullPath.Replace(HttpRuntime.AppDomainAppPath + ConfigUtil.FileDirectory, "");
                //Luannv6 sửa lại luôn truyền về filepath kể cả không có dữ liệu
                if (in_item == null || !in_item.HasListData)
                {
                    return new ResultInfor<T>()
                    {
                        ERR_CODE = fullPath,
                        rtResult = false,
                        AddMSG2 = "Không có dữ liệu"
                    };
                }

                return new ResultInfor<T>()
                {
                    ERR_CODE = handle,
                    rtResult = true,
                    AddMSG2 = "Xuất excel thành công."
                };
            }
            catch (Exception ex)
            {
                return new ResultInfor<T>(false, "Lỗi xuất excel." + ex.ToString());
            }
        }

        //public async static Task<ResultInfor<T>> ExportExcelFromListAsync<T>(string fileName, string templateFileName, int rowStart, ResultInfor<T> in_item) where T : new()
        //{
        //    try
        //    {
        //        var rt = ExcelUtil.ResponeExcelFromList<T>(fileName, templateFileName, rowStart, in_item.ListData);
        //        string handle = Guid.NewGuid().ToString();

        //        var result = await RedisCacheProvider.GetRedis().InsertAsync(handle, rt);

        //        return new ResultInfor<T>()
        //        {
        //            ERR_CODE = handle,
        //            rtResult = result,
        //            AddMSG2 = (result == true ? "Xuất excel thành công." : "Xuất excel không thành công.")
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ResultInfor<T>(false, "Lỗi xuất excel." + ex.ToString());
        //    }
        //}

        /// <summary>
        /// Đọc file excel
        /// </summary>
        /// <typeparam name="T">Object cần set dữ liệu</typeparam>
        /// <param name="pathFileExcel">Đường dẫn file excel</param>
        /// <param name="rowStart">Dòng bắt đầu dữ liệu</param>
        /// <param name="sheetName">Tên sheet</param>
        /// <returns>List object</returns>
        /// <author>luannv6 - 04-07-2018</author>
        private static string CreateFileExcel<T>(string fileName, string templatePath, int rowStart, List<T> lstData) where T : new()
        {
            string rt = "";
            T typeOject = new T();
            string pathTemplate = templatePath;
            string fullPath = CloneTemplateFile(pathTemplate, HttpRuntime.AppDomainAppPath + ConfigUtil.FileDirectory + "\\FileExport", fileName);
            try
            {
                FileInfo excelFile = new FileInfo(fullPath);

                bool validExcel = false;
                bool existSTT = false;
                using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
                {
                    List<PropertyInfo> mpropertyInfos = typeOject.GetType().GetProperties().ToList();

                    if (excelPackage.Workbook.Worksheets.Count > 0)
                    {
                        //Kiểm tra sheet name 
                        ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.ElementAt(0);
                        if (workSheet != null)
                        {
                            List<int> lstColumExcel = new List<int>();
                            //Lấy về danh sách các thuộc tính có description trùng tên cột excel
                            List<PropertyInfo> lstProperties = GetListPropertiesMatch(workSheet, mpropertyInfos, out validExcel, out existSTT, 1);
                            //Nếu có ít nhất 1 thuộc tính của object trùng với tên cột trên excel thì mới xử lý điền dữ liệu
                            if (validExcel)
                            {
                                // điền dữ liệu bắt đầu từ row thứ rowStart
                                for (int i = 0; i < lstData.Count; i++)
                                {
                                    T obj = lstData[i];
                                    //Nếu có cột số thứ tự thì điền stt
                                    if (existSTT)
                                        workSheet.Cells[rowStart + i, 1].Value = i + 1;

                                    for (int j = 0; j < lstProperties.Count; j++)
                                    {
                                        if (lstProperties[j] != null)
                                            workSheet.Cells[rowStart + i, j + 1].Value = obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null);
                                    }
                                }
                                excelPackage.Save();


                                rt = fullPath;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rt;
        }
        #endregion

        #region Respone file excel
        /// <summary>
        /// Hàm trả về file excel cho client từ 1 list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="templateFileName"></param>
        /// <param name="rowStart"></param>
        /// <param name="lstData"></param>
        /// <param name="respone"></param>
        /// <returns></returns>
        public static byte[] ResponeExcelFromList<T>(string fileName, string templateFileName, int rowStart, List<T> lstData) where T : new()
        {
            //tmp_ExportTemp
            try
            {
                byte[] isExported = CreateFileExcelRespone(fileName, HttpRuntime.AppDomainAppPath + ConfigUtil.tmp_ExportTemp + templateFileName, rowStart, lstData);
                return isExported;
            }
            catch (Exception ex)
            {
                LogUtil.Error(Libs.SerializeObject(new
                {
                    fileName = fileName,
                    templateFileName = templateFileName,
                    rowStart = rowStart,
                    lstData = lstData
                }), ex);
                return new byte[] { };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="templatePath"></param>
        /// <param name="rowStart"></param>
        /// <param name="lstData"></param>
        /// <returns></returns>
        public static byte[] CreateFileExcelRespone<T>(string fileName, string templatePath, int rowStart, List<T> lstData) where T : new()
        {
            byte[] rt = null;
            T typeOject = new T();
            string pathTemplate = templatePath;
            string fullPath = templatePath;//CloneTemplateFile(pathTemplate, HttpRuntime.AppDomainAppPath + ConfigUtil.FileDirectory + "\\FileExport", fileName);
            try
            {
                FileInfo excelFile = new FileInfo(fullPath);

                bool validExcel = false;
                bool existSTT = false;
                using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
                {
                    List<PropertyInfo> mpropertyInfos = typeOject.GetType().GetProperties().ToList();

                    if (excelPackage.Workbook.Worksheets.Count > 0)
                    {
                        //Kiểm tra sheet name 
                        ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.ElementAt(0);
                        if (workSheet != null)
                        {
                            List<int> lstColumExcel = new List<int>();
                            //Lấy về danh sách các thuộc tính có description trùng tên cột excel
                            List<PropertyInfo> lstProperties = GetListPropertiesMatch(workSheet, mpropertyInfos, out validExcel, out existSTT, 1);
                            //Nếu có ít nhất 1 thuộc tính của object trùng với tên cột trên excel thì mới xử lý điền dữ liệu
                            if (validExcel)
                            {
                                // điền dữ liệu bắt đầu từ row thứ rowStart
                                for (int i = 0; i < lstData.Count; i++)
                                {
                                    T obj = lstData[i];
                                    //Nếu có cột số thứ tự thì điền stt
                                    if (existSTT)
                                        workSheet.Cells[rowStart + i, 1].Value = i + 1;

                                    for (int j = 0; j < lstProperties.Count; j++)
                                    {
                                        if (lstProperties[j] != null)
                                        {
                                            string typeName = obj.GetType().GetProperty(lstProperties[j].Name).PropertyType.Name.ToLower();

                                            // Add border for file excel
                                            workSheet.Cells[rowStart + i, j + 1].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                            workSheet.Cells[rowStart + i, j + 1].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                            workSheet.Cells[rowStart + i, j + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                            workSheet.Cells[rowStart + i, j + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                            switch (typeName)
                                            {
                                                case "short":
                                                    workSheet.Cells[rowStart + i, j + 1].Value = Convert.ToInt16(obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null));
                                                    break;
                                                case "int":
                                                    workSheet.Cells[rowStart + i, j + 1].Value = Convert.ToInt32(obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null));
                                                    break;
                                                case "long":
                                                    workSheet.Cells[rowStart + i, j + 1].Value = Convert.ToInt64(obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null));
                                                    break;
                                                case "int16":
                                                    workSheet.Cells[rowStart + i, j + 1].Value = Convert.ToInt16(obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null));
                                                    break;
                                                case "int32":
                                                    workSheet.Cells[rowStart + i, j + 1].Value = Convert.ToInt32(obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null));
                                                    break;
                                                case "int64":
                                                    workSheet.Cells[rowStart + i, j + 1].Value = Convert.ToInt64(obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null));
                                                    break;
                                                default:
                                                    //trường hợp object có kiểu con bên trong là một list
                                                    if (typeName.StartsWith("list"))
                                                    {
                                                        //lấy giá trị list ra
                                                        var listObj = (IList)obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null);
                                                        if (null != listObj && listObj.Count > 0)
                                                        {
                                                            //lấy danh sách properties của phần tử đầu tiên trong listproperties
                                                            List<PropertyInfo> subProperties = listObj[0].GetType().GetProperties().ToList();
                                                            //dòng bắt đầu ghi xẽ nằm dưới dòng của object cha
                                                            int subRowStart = rowStart + i + 1;
                                                            int columnStart = 2;
                                                            //viet ten cot trong excel(lay description lam ten cot)
                                                            for (int a = 0; a < subProperties.Count; a++)
                                                            {
                                                                var arrAttr = subProperties[a].GetCustomAttributes(typeof(DescriptionAttribute), true);
                                                                if (arrAttr == null || arrAttr.Length <= 0)
                                                                {
                                                                    continue;
                                                                }
                                                                DescriptionAttribute attrName = (DescriptionAttribute)arrAttr[0];
                                                                workSheet.Cells[subRowStart, columnStart + a].Style.Font.Size = 14;
                                                                workSheet.Cells[subRowStart, columnStart + a].Style.Font.Bold = true;
                                                                workSheet.Cells[subRowStart, columnStart + a].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                                                workSheet.Cells[subRowStart, columnStart + a].Value = attrName.Description;
                                                            }
                                                            subRowStart = subRowStart + 1;

                                                            if (null != subProperties && subProperties.Count > 0)
                                                            {
                                                                for (int z = 0; z < listObj.Count; z++)
                                                                {
                                                                    object oj = listObj[z];
                                                                    for (int k = 0; k < subProperties.Count; k++)
                                                                    {
                                                                        if (subProperties[k] != null)
                                                                        {
                                                                            string subTypeName = oj.GetType().GetProperty(subProperties[k].Name).PropertyType.Name.ToLower();
                                                                            var arrAttr = subProperties[k].GetCustomAttributes(typeof(DescriptionAttribute), true);
                                                                            if (arrAttr == null || arrAttr.Length <= 0)
                                                                            {
                                                                                //Lấy description của thuộc tính nếu bằng tên cột trên file excel thì add vào list thuộc tính sẽ set giá trị
                                                                                continue;
                                                                            }
                                                                            DescriptionAttribute attrName = (DescriptionAttribute)arrAttr[0];
                                                                            if (!string.IsNullOrEmpty(attrName.Description))
                                                                            {
                                                                                switch (subTypeName)
                                                                                {
                                                                                    case "short":
                                                                                        try
                                                                                        {
                                                                                            workSheet.Cells[subRowStart + z, columnStart + k].Value = Convert.ToInt16(oj.GetType().GetProperty(lstProperties[k].Name).GetValue(oj, null));
                                                                                        }
                                                                                        catch
                                                                                        {

                                                                                        }
                                                                                        break;
                                                                                    case "int":
                                                                                        try
                                                                                        {
                                                                                            workSheet.Cells[subRowStart + z, columnStart + k].Value = Convert.ToInt32(oj.GetType().GetProperty(lstProperties[k].Name).GetValue(oj, null));
                                                                                        }
                                                                                        catch
                                                                                        {

                                                                                        }
                                                                                        break;
                                                                                    case "long":
                                                                                        try
                                                                                        {
                                                                                            workSheet.Cells[subRowStart + z, columnStart + k].Value = Convert.ToInt64(oj.GetType().GetProperty(lstProperties[k].Name).GetValue(oj, null));
                                                                                        }
                                                                                        catch
                                                                                        {

                                                                                        }
                                                                                        break;
                                                                                    case "int16":
                                                                                        try
                                                                                        {
                                                                                            workSheet.Cells[subRowStart + z, columnStart + k].Value = Convert.ToInt16(oj.GetType().GetProperty(lstProperties[k].Name).GetValue(oj, null));
                                                                                        }
                                                                                        catch
                                                                                        {

                                                                                        }
                                                                                        break;
                                                                                    case "int32":
                                                                                        try
                                                                                        {
                                                                                            workSheet.Cells[subRowStart + z, columnStart + k].Value = Convert.ToInt32(oj.GetType().GetProperty(lstProperties[k].Name).GetValue(oj, null));
                                                                                        }
                                                                                        catch
                                                                                        {

                                                                                        }
                                                                                        break;
                                                                                    case "int64":
                                                                                        try
                                                                                        {
                                                                                            workSheet.Cells[subRowStart + z, columnStart + k].Value = Convert.ToInt64(oj.GetType().GetProperty(lstProperties[k].Name).GetValue(oj, null));
                                                                                        }
                                                                                        catch
                                                                                        {

                                                                                        }
                                                                                        break;
                                                                                    default:
                                                                                        try
                                                                                        {
                                                                                            workSheet.Cells[subRowStart + z, columnStart + k].Value = Convert.ToString(oj.GetType().GetProperty(lstProperties[k].Name).GetValue(oj, null));
                                                                                        }
                                                                                        catch
                                                                                        {

                                                                                        }
                                                                                        break;
                                                                                }
                                                                            }

                                                                        }
                                                                    }
                                                                }
                                                                rowStart = rowStart + i + subProperties.Count;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        workSheet.Cells[rowStart + i, j + 1].Value = Convert.ToString(obj.GetType().GetProperty(lstProperties[j].Name).GetValue(obj, null));
                                                    }
                                                    break;
                                            }

                                            if (workSheet.Cells[rowStart + i, j + 1].Style.Numberformat.Format != "General")
                                            {
                                                var copyFormat = workSheet.Cells[rowStart + i, j + 1].Style.Numberformat.Format;
                                                workSheet.Cells[rowStart + i, j + 1].Style.Numberformat.Format = "General";
                                                workSheet.Cells[rowStart + i, j + 1].Style.Numberformat.Format = copyFormat;
                                            }
                                        }
                                    }
                                }
                                //excelPackage.Save();

                                //rt = fullPath;
                                //rt = true;
                                //var stream = new MemoryStream(excelPackage.GetAsByteArray(), 0, 0, true, true);
                                rt = excelPackage.GetAsByteArray();
                                //respone.Clear();
                                //respone.AddHeader("content-disposition", "attachment;filename=" + fileName);
                                //respone.ContentType = "application/vnd.ms-excel";
                                //respone.OutputStream.Write(buffer, 0, buffer.Length);
                                //respone.Flush();
                                //respone.End();

                            }
                        }
                    }
                }

                return rt;
            }
            catch (Exception ex)
            {
                LogUtil.Error(Libs.SerializeObject(new
                {
                    fileName = fileName,
                    templatePath = templatePath,
                    rowStart = rowStart,
                    lstData = lstData
                }), ex);
                return new byte[] { };
            }

        }
        #endregion

        #region private method
        /// <summary>
        /// Đọc file excel
        /// </summary>
        /// <typeparam name="T">Object cần set dữ liệu</typeparam>
        /// <param name="pathFileExcel">Đường dẫn file excel</param>
        /// <param name="rowStart">Dòng bắt đầu dữ liệu</param>
        /// <param name="sheetName">Tên sheet</param>
        /// <returns>List object</returns>
        /// <author>luannv6 - 04-07-2018</author>
        private static ResultInfor<T> ReadFileExcel2003<T>(Stream excelFileStream, int rowStart, string sheetName) where T : new()
        {
            var rt = new ResultInfor<T>();
            List<T> result = new List<T>();
            T typeOject = new T();
            try
            {
                HSSFWorkbook wb = new HSSFWorkbook(excelFileStream, true);


                var validSheetName = false;
                bool validExcel = false;
                bool existSTT = false;

                List<PropertyInfo> mpropertyInfos = typeOject.GetType().GetProperties().ToList();

                for (int i = 0; i < wb.Workbook.NumSheets; i++)
                {

                    //Nếu không truyền tên sheet thì lấy mặc định sheet đầu tiên
                    if (string.IsNullOrEmpty(sheetName))
                        sheetName = wb.GetSheetAt(0).SheetName;
                    //Kiểm tra sheet name 
                    if (wb.GetSheetAt(i).SheetName == sheetName)
                    {
                        HSSFSheet sh = (HSSFSheet)wb.GetSheetAt(i);
                        validSheetName = true;
                        List<int> lstColumExcel = new List<int>();
                        //Lấy về danh sách các thuộc tính có description trùng tên cột excel
                        List<PropertyInfo> lstProperties = GetListPropertiesMatch(sh, mpropertyInfos, out validExcel, out existSTT);
                        Dictionary<string, string> dicError = new Dictionary<string, string>();
                        //Nếu có ít nhất 1 thuộc tính của object trùng với tên cột trên excel thì mới xử lý lấy dữ liệu
                        if (validExcel)
                        {
                            // Đọc tất cả data bắt đầu từ row thứ rowStart
                            for (var rowNumber = rowStart; rowNumber <= sh.PhysicalNumberOfRows; rowNumber++)
                            {
                                // Lấy 1 row trong excel để truy vấn
                                var row = sh.GetRow(rowNumber - 1);
                                if (row != null)
                                {
                                    //set giá trị cho object T
                                    T obj = SetValueToObject<T>(row, lstProperties, dicError, out dicError, rowNumber - 1);
                                    //Add obj vào list trả ra
                                    result.Add(obj);
                                }
                            }
                            if (dicError.Count > 0)
                            {
                                rt.ListData = result;
                                rt.rtResult = false;
                                foreach (var dic in dicError)
                                {
                                    rt.vListMSG.Append(dic.Value);
                                }

                            }
                            else
                            {
                                rt.ListData = result;
                                rt.rtResult = true;
                                rt.vListMSG.Append("Đọc dữ liệu thành công.");
                            }
                        }
                    }
                }
                if (!validSheetName || !validExcel)
                {
                    rt.rtResult = false;
                    string msg = (!validSheetName) ? "Không tìm được tên sheet '" + sheetName + "'" : "File mẫu excel không đúng định dạng.";
                    rt.vListMSG.Append(msg);
                }
            }
            catch (Exception)
            {
                rt.rtResult = false;
                rt.vListMSG.Append("Lỗi đọc file excel.");
            }

            return rt;
        }

        /// <summary>
        /// Đọc file excel
        /// </summary>
        /// <typeparam name="T">Object cần set dữ liệu</typeparam>
        /// <param name="pathFileExcel">Đường dẫn file excel</param>
        /// <param name="rowStart">Dòng bắt đầu dữ liệu</param>
        /// <param name="sheetName">Tên sheet</param>
        /// <returns>List object</returns>
        /// <author>luannv6 - 04-07-2018</author>
        private static ResultInfor<T> ReadFileExcel2007<T>(Stream excelFileStream, int rowStart, string sheetName) where T : new()
        {
            var rt = new ResultInfor<T>();
            List<T> result = new List<T>();
            T typeOject = new T();
            try
            {
                var validSheetName = false;
                bool validExcel = false;
                bool existSTT = false;
                using (ExcelPackage excelPackage = new ExcelPackage(excelFileStream))
                {
                    List<PropertyInfo> mpropertyInfos = typeOject.GetType().GetProperties().ToList();

                    for (int i = 0; i < excelPackage.Workbook.Worksheets.Count; i++)
                    {
                        //Nếu không truyền tên sheet thì lấy mặc định sheet đầu tiên
                        if (string.IsNullOrEmpty(sheetName))
                            sheetName = excelPackage.Workbook.Worksheets.ElementAt(0).Name;
                        //Kiểm tra sheet name 
                        ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.ElementAt(i);
                        if (workSheet.Name == sheetName)
                        {
                            validSheetName = true;
                            List<int> lstColumExcel = new List<int>();
                            //Lấy về danh sách các thuộc tính có description trùng tên cột excel
                            List<PropertyInfo> lstProperties = GetListPropertiesMatch(workSheet, mpropertyInfos, out validExcel, out existSTT, 0);
                            //Nếu có ít nhất 1 thuộc tính của object trùng với tên cột trên excel thì mới xử lý lấy dữ liệu
                            Dictionary<string, string> dicError = new Dictionary<string, string>();
                            if (validExcel)
                            {
                                // Đọc tất cả data bắt đầu từ row thứ rowStart
                                for (var rowNumber = rowStart; rowNumber <= workSheet.Dimension.End.Row; rowNumber++)
                                {
                                    // Lấy 1 row trong excel để truy vấn
                                    var row = workSheet.Cells[rowNumber, 1, rowNumber, workSheet.Dimension.End.Column];
                                    //set giá trị cho object T
                                    T obj = SetValueToObject<T>(row, lstProperties, dicError, out dicError, rowNumber - 1);
                                    //Add obj vào list trả ra
                                    result.Add(obj);
                                }
                                if (dicError.Count > 0)
                                {
                                    rt.ListData = result;
                                    rt.rtResult = false;
                                    foreach (var dic in dicError)
                                    {
                                        rt.vListMSG.Append(dic.Value);
                                    }

                                }
                                else
                                {
                                    rt.ListData = result;
                                    rt.rtResult = true;
                                    rt.vListMSG.Append("Đọc dữ liệu thành công.");
                                }

                            }
                        }
                    }
                    if (!validSheetName || !validExcel)
                    {
                        rt.rtResult = false;
                        string msg = (!validSheetName) ? "Không tìm được tên sheet '" + sheetName + "'" : "File mẫu excel không đúng định dạng.";
                        rt.vListMSG.Append(msg);
                    }
                }
            }
            catch (Exception)
            {
                rt.rtResult = false;
                rt.vListMSG.Append("Lỗi đọc file excel.");
            }

            return rt;
        }

        /// <summary>
        /// Hàm lấy ra list properties có description trùng với tên cột excel
        /// </summary>
        /// <param name="workSheet">Work sheet</param>
        /// <param name="mpropertyInfos"> danh sách properties của object</param>
        /// <param name="validExcel">Biến kiểm tra nếu có ít nhất 1 thuộc tính trùng tên thì out true| ngược lại out false</param> 
        /// <param name="ProType">Property 0: for CanWrite, 1: for CanRead</param> 
        /// <returns>Danh sách thuộc tính match</returns>
        /// <author>luannv6 - 04-07-2018</author>
        private static List<PropertyInfo> GetListPropertiesMatch(ExcelWorksheet workSheet, List<PropertyInfo> mpropertyInfos, out bool validExcel, out bool existSTT, int ProType = 0)
        {
            List<PropertyInfo> lstProperties = new List<PropertyInfo>();
            validExcel = false;
            existSTT = false;
            // Đọc tất cả các header và add list properties tương ứng từng cột theo thứ tự trong file excel
            foreach (var firstRowCell in workSheet.Cells[1, 1, 1, workSheet.Dimension.End.Column])
            {
                bool isExist = false;
                if (ProType == 1)
                {
                    foreach (PropertyInfo prt in mpropertyInfos.FindAll(c => c.CanRead))
                    {
                        if (!existSTT && firstRowCell.Text.Trim().ToUpper() == "STT")
                            existSTT = true;
                        var arrAttr = prt.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (arrAttr != null && arrAttr.Length > 0)
                        {
                            //Lấy description của thuộc tính nếu bằng tên cột trên file excel thì add vào list thuộc tính sẽ set giá trị
                            DescriptionAttribute attrName = (DescriptionAttribute)arrAttr[0];
                            if (attrName.Description.ToString().Trim().ToLower() == firstRowCell.Text.Replace("*", "").Trim().ToLower())
                            {
                                lstProperties.Add(prt);
                                isExist = true;
                                validExcel = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (PropertyInfo prt in mpropertyInfos.FindAll(c => c.CanWrite))
                    {
                        if (!existSTT && firstRowCell.Text.Trim().ToUpper() == "STT")
                            existSTT = true;
                        var arrAttr = prt.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (arrAttr != null && arrAttr.Length > 0)
                        {
                            //Lấy description của thuộc tính nếu bằng tên cột trên file excel thì add vào list thuộc tính sẽ set giá trị
                            DescriptionAttribute attrName = (DescriptionAttribute)arrAttr[0];
                            if (attrName.Description.ToString().Trim().ToLower() == firstRowCell.Text.Replace("*", "").Trim().ToLower())
                            {
                                lstProperties.Add(prt);
                                isExist = true;
                                validExcel = true;
                                break;
                            }
                        }
                    }
                }


                //Nếu lặp tất cả các thuộc tính mà không có cột nào có description trùng tên cột excel thì add null
                if (!isExist)
                    lstProperties.Add(null);
            }
            return lstProperties;
        }
        /// <summary>
        /// Hàm lấy ra list properties có description trùng với tên cột excel
        /// </summary>
        /// <param name="workSheet">Work sheet</param>
        /// <param name="mpropertyInfos"> danh sách properties của object</param>
        /// <param name="validExcel">Biến kiểm tra nếu có ít nhất 1 thuộc tính trùng tên thì out true| ngược lại out false</param>
        /// <returns>Danh sách thuộc tính match</returns>
        /// <author>luannv6 - 04-07-2018</author>
        private static List<PropertyInfo> GetListPropertiesMatch(HSSFSheet sh, List<PropertyInfo> mpropertyInfos, out bool validExcel, out bool existSTT)
        {
            List<PropertyInfo> lstProperties = new List<PropertyInfo>();
            validExcel = false;
            existSTT = false;
            // Đọc tất cả các header và add list properties tương ứng từng cột theo thứ tự trong file excel
            // Tao cot cho datatable
            if (sh.GetRow(0) != null)
            {
                for (int j = 0; j <= sh.GetRow(0).Cells.Count - 1; j++)
                {
                    var cell = sh.GetRow(0).GetCell(j);
                    bool isExist = false;
                    foreach (PropertyInfo prt in mpropertyInfos.FindAll(c => c.CanWrite))
                    {
                        if (!existSTT && cell.StringCellValue.Trim().ToUpper() == "STT")
                            existSTT = true;
                        var arrAttr = prt.GetCustomAttributes(typeof(DescriptionAttribute), true);
                        if (arrAttr != null && arrAttr.Length > 0)
                        {
                            //Lấy description của thuộc tính nếu bằng tên cột trên file excel thì add vào list thuộc tính sẽ set giá trị
                            DescriptionAttribute attrName = (DescriptionAttribute)arrAttr[0];
                            if (attrName.Description.ToString().Trim().ToLower() == cell.StringCellValue.Replace("*", "").Trim().ToLower())
                            {
                                lstProperties.Add(prt);
                                isExist = true;
                                validExcel = true;
                                break;
                            }
                        }
                    }
                    //Nếu lặp tất cả các thuộc tính mà không có cột nào có description trùng tên cột excel thì add null
                    if (!isExist)
                        lstProperties.Add(null);
                }
            }
            return lstProperties;
        }
        /// <summary>
        /// Hàm set giá trị cho object từ 1 dòng excel
        /// </summary>
        /// <typeparam name="T">Kiểu object</typeparam>
        /// <param name="row">Dòng dữ liệu</param>
        /// <param name="lstProperties">Danh sách thuộc tính của object</param>
        /// <param name="obj">Object cần set dữ liệu</param>
        /// <returns>Object</returns>
        /// <author>luannv6 - 04-07-2018</author>
        private static T SetValueToObject<T>(ExcelRange row, List<PropertyInfo> lstProperties, Dictionary<string, string> oldError, out Dictionary<string, string> dicError, int rowNumber) where T : new()
        {
            T obj = new T();
            dicError = oldError;
            for (int k = 0; k < row.Columns; k++)
            {
                PropertyInfo prop = null;
                if (lstProperties.Count > k)
                    prop = lstProperties[k];

                if (prop != null)
                {
                    try
                    {
                        var value = ((object[,])row.Value)[0, k];
                        if (value == null)
                            continue;
                        //Try phần set giá trị để nếu trường nào không set được thì bỏ qua. validate sau
                        Type pType = DataMapper.GetPropertyType(prop.PropertyType);
                        Type vType = DataMapper.GetPropertyType(value.GetType());
                        if (pType.Equals(vType))
                        {
                            // types match, just copy value
                            prop.SetValue(obj, value, null);
                        }
                        else
                        {
                            // types don't match, try to coerce
                            if (pType.Equals(typeof(Guid)))
                                prop.SetValue(obj, new Guid(value.ToString()), null);
                            else if (pType.IsEnum && vType.Equals(typeof(string)))
                                prop.SetValue(obj, Enum.Parse(pType, value.ToString()), null);
                            else if (pType.Equals(typeof(string)))
                                prop.SetValue(obj, Convert.ToString(value), null);
                            else if (pType.Equals(typeof(DateTime)))
                            {
                                DateTime dt;
                                if (DateTime.TryParseExact(value.ToString(),
                                                            "d/M/yyyy",
                                                            System.Globalization.CultureInfo.InvariantCulture,
                                                            System.Globalization.DateTimeStyles.None,
                                    out dt))
                                {
                                    prop.SetValue(obj, dt, null);
                                }
                            }

                            else
                                prop.SetValue(obj, Convert.ChangeType(value.ToString(), pType), null);
                        }
                    }
                    catch (Exception)
                    {
                        if (!dicError.ContainsKey(prop.Name))
                        {
                            var arrAttr = prop.GetCustomAttributes(typeof(DescriptionAttribute), true);
                            if (arrAttr != null && arrAttr.Length > 0)
                            {
                                //Lấy description của thuộc tính nếu bằng tên cột trên file excel thì add vào list thuộc tính sẽ set giá trị
                                DescriptionAttribute attrName = (DescriptionAttribute)arrAttr[0];
                                dicError.Add(prop.Name, attrName.Description + " không hợp lệ tại dòng: " + rowNumber.ToString());
                            }
                        }
                        else
                        {
                            dicError[prop.Name] = dicError[prop.Name] + ", " + rowNumber.ToString();
                        }
                    }
                }
                //k++;
            }
            return obj;
        }

        /// <summary>
        /// Hàm set giá trị cho object từ 1 dòng excel
        /// </summary>
        /// <typeparam name="T">Kiểu object</typeparam>
        /// <param name="row">Dòng dữ liệu</param>
        /// <param name="lstProperties">Danh sách thuộc tính của object</param>
        /// <param name="obj">Object cần set dữ liệu</param>
        /// <returns>Object</returns>
        /// <author>luannv6 - 04-07-2018</author>
        private static T SetValueToObject<T>(IRow row, List<PropertyInfo> lstProperties, Dictionary<string, string> oldError, out Dictionary<string, string> dicError, int rowNumber) where T : new()
        {
            T obj = new T();
            dicError = oldError;
            for (int k = 0; k <= row.Cells.Count; k++)
            {
                PropertyInfo prop = null;
                if (lstProperties.Count > k)
                    prop = lstProperties[k];
                var cell = row.GetCell(k);
                if (prop != null && cell != null)
                {
                    try
                    {

                        Type vType = typeof(string);
                        object value = null;
                        switch (cell.CellType)
                        {
                            case CellType.Numeric:
                                {
                                    if (HSSFDateUtil.IsCellDateFormatted(cell))
                                    {
                                        vType = typeof(DateTime);
                                        value = cell.DateCellValue.ToString("dd/MM/yyyy");
                                    }
                                    else
                                    {
                                        vType = typeof(decimal);
                                        value = cell.NumericCellValue;
                                    }
                                    break;
                                }
                            default:
                                {
                                    vType = typeof(string);
                                    value = cell.StringCellValue;
                                    break;
                                }
                        }

                        if (value == null)
                            continue;
                        //Try phần set giá trị để nếu trường nào không set được thì bỏ qua. validate sau
                        Type pType = DataMapper.GetPropertyType(prop.PropertyType);
                        if (pType.Equals(vType))
                        {
                            // types match, just copy value
                            prop.SetValue(obj, value, null);
                        }
                        else
                        {
                            // types don't match, try to coerce
                            if (pType.Equals(typeof(Guid)))
                                prop.SetValue(obj, new Guid(value.ToString()), null);
                            else if (pType.IsEnum && vType.Equals(typeof(string)))
                                prop.SetValue(obj, Enum.Parse(pType, value.ToString()), null);
                            else if (pType.Equals(typeof(string)))
                                prop.SetValue(obj, Convert.ToString(value), null);
                            else if (pType.Equals(typeof(DateTime)))
                            {
                                DateTime dt;
                                if (DateTime.TryParseExact(value.ToString(),
                                                            "d/M/yyyy",
                                                            System.Globalization.CultureInfo.InvariantCulture,
                                                            System.Globalization.DateTimeStyles.None,
                                    out dt))
                                {
                                    prop.SetValue(obj, dt, null);
                                }
                            }

                            else
                                prop.SetValue(obj, Convert.ChangeType(value.ToString(), pType), null);
                        }
                    }
                    catch (Exception)
                    {
                        if (!dicError.ContainsKey(prop.Name))
                        {
                            var arrAttr = prop.GetCustomAttributes(typeof(DescriptionAttribute), true);
                            if (arrAttr != null && arrAttr.Length > 0)
                            {
                                //Lấy description của thuộc tính nếu bằng tên cột trên file excel thì add vào list thuộc tính sẽ set giá trị
                                DescriptionAttribute attrName = (DescriptionAttribute)arrAttr[0];
                                dicError.Add(prop.Name, attrName.Description + " không hợp lệ tại dòng: " + rowNumber.ToString());
                            }
                        }
                        else
                        {
                            dicError[prop.Name] = dicError[prop.Name] + ", " + rowNumber.ToString();
                        }
                    }
                }
                //k++;
            }
            return obj;
        }

        /// <summary>
        /// Hàm copy file ra 1 thư mục khác trước khi fill dữ liệu
        /// </summary>
        /// <param name="sourcePath">Đường dẫn file gốc</param>
        /// <param name="destinationPath">Đường dẫn file copy</param>
        /// <param name="sOutputName">Tên file sau khi copy</param>
        /// <returns>Đường dẫn đầy đủ của file sau khi copy xong</returns>
        private static string CloneTemplateFile(string sourcePath, string destinationPath, string sOutputName)
        {
            string fullPath = "";
            string sourceFile = System.IO.Path.Combine(sourcePath);
            //Cộng thêm ngày giờ ghi file trên server để không bị trùng.            
            sOutputName = DateTime.Now.ToString("yyyyMMddHHmmssFFF") + sOutputName;

            fullPath = System.IO.Path.Combine(destinationPath, sOutputName);

            if (!System.IO.Directory.Exists(destinationPath))
            {
                System.IO.Directory.CreateDirectory(destinationPath);
            }
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            System.IO.File.Copy(sourceFile, fullPath, true);

            FileInfo file = new FileInfo(fullPath);
            file.IsReadOnly = false;

            return fullPath;
        }

        /// <summary>
        /// Hàm trả về file excel cho client từ 1 list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="templateFileName"></param>
        /// <param name="rowStart"></param>
        /// <param name="lstData"></param>
        /// <param name="respone"></param>
        /// <returns></returns>
        public static byte[] ResponeExcelFromListNotDependOnDescription<T>(string fileName, string templateFileName, int rowStart, int columnStart, List<T> lstData) where T : new()
        {
            //tmp_ExportTemp

            try
            {


                byte[] isExported = CreateFileExcelResponeNotDependOnDescription(fileName, HttpRuntime.AppDomainAppPath + ConfigUtil.tmp_ExportTemp + templateFileName, rowStart, columnStart, lstData);
                return isExported;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static byte[] CreateFileExcelResponeNotDependOnDescription<T>(string fileName, string templatePath, int rowStart, int columnStart, List<T> lstData) where T : new()
        {
            byte[] rt = null;
            T typeOject = new T();
            string pathTemplate = templatePath;
            string fullPath = templatePath;//CloneTemplateFile(pathTemplate, HttpRuntime.AppDomainAppPath + ConfigUtil.FileDirectory + "\\FileExport", fileName);
            try
            {
                FileInfo excelFile = new FileInfo(fullPath);

                using (ExcelPackage excelPackage = new ExcelPackage(excelFile))
                {
                    List<PropertyInfo> mpropertyInfos = typeOject.GetType().GetProperties().ToList();

                    if (excelPackage.Workbook.Worksheets.Count > 0)
                    {
                        //Kiểm tra sheet name 
                        ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets.ElementAt(0);
                        if (workSheet != null)
                        {
                            //Lấy về danh sách các thuộc tính có description trùng tên cột excel
                            //List<PropertyInfo> lstProperties = GetListPropertiesMatch(workSheet, mpropertyInfos, out validExcel, out existSTT, 1);
                            // điền dữ liệu bắt đầu từ row thứ rowStart
                            if (columnStart == 0)
                            {
                                columnStart = 1;
                            }
                            for (int i = 0; i < lstData.Count; i++)
                            {
                                T obj = lstData[i];
                                int startWiteColumn = columnStart;
                                for (int j = 0; j < mpropertyInfos.Count; j++)
                                {
                                    if (mpropertyInfos[j] != null)
                                    {
                                        workSheet.Cells[rowStart + i, startWiteColumn].Value = Convert.ToString(obj.GetType().GetProperty(mpropertyInfos[j].Name).GetValue(obj, null));

                                        // Add border for excel
                                        workSheet.Cells[rowStart + i, startWiteColumn].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                                        workSheet.Cells[rowStart + i, startWiteColumn].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                                        workSheet.Cells[rowStart + i, startWiteColumn].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                        workSheet.Cells[rowStart + i, startWiteColumn].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                                    }
                                    startWiteColumn++;
                                }
                            }

                            rt = excelPackage.GetAsByteArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return rt;
        }

        #endregion      
    }
}