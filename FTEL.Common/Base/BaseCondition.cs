using FTEL.Common.SqlService;
using FTEL.Common.Utilities;
using System;
using System.Collections.Generic;

namespace FTEL.Common.Base
{
    public class BaseCondition
    {

        public string mIN_WHERE { get; set; }
        public string IN_SORT { get; set; }
        public List<FilterItems> GetfilterRules { get; set; }
        public string IN_WHERE
        {
            get
            {
                if (GetfilterRules == null || GetfilterRules.Count == 0)
                {
                    return mIN_WHERE;
                }
                else
                {
                    var result = new System.Text.StringBuilder(mIN_WHERE);
                    foreach (var item in GetfilterRules)
                    {
                        var valueKillSqlInjection = Libs.KillSqlInjection(item.value);
                        if (!string.IsNullOrEmpty(valueKillSqlInjection))
                        {
                            /*
                             * Type: single string value
                             * Condition type: AND
                             */
                            if (item.op == "and_contains")
                            {
                                //result.Append(" AND LOWER(" + item.field + ") LIKE N'%" + valueKillSqlInjection.ToLower().Replace(" ", "%") + "%'");
                                result.Append(" AND LOWER(" + item.field + ") LIKE N'%" + valueKillSqlInjection.ToLower().Trim() + "%'");
                            }

                            /*
                             * Type: single string value
                             * Condition type: AND
                             */
                            if (item.op == "or_contains")
                            {
                                result.Append(" OR LOWER(" + item.field + ") LIKE N'%" + valueKillSqlInjection.ToLower().Trim() + "%'");
                            }

                            /*
                             * Type: single string value
                             * Condition type: AND
                             */
                            else if (item.op == "and_in_string")
                            {
                                result.Append(" And " + item.field + " IN (N'" + valueKillSqlInjection + "')");
                            }

                            /*
                             * Type: single int value
                             * Condition type: AND
                             */
                            else if (item.op == "and_in_int")
                            {
                                result.Append(" And " + item.field + " IN (" + valueKillSqlInjection + ")");
                            }

                            /*
                             * Type: list string value
                             * Condition type: AND
                             */
                            else if (item.op == "and_in_strings")
                            {
                                if (valueKillSqlInjection.IndexOf(",") > 0)
                                {
                                    valueKillSqlInjection = valueKillSqlInjection.ToLower().Replace(",", "',N'");
                                }

                                result.Append(" And " + item.field + " IN (N'" + valueKillSqlInjection + "')");
                            }

                            /*
                             * Type: list int value
                             * Condition type: AND
                             */
                            else if (item.op == "and_in_ints")
                            {
                                if (valueKillSqlInjection.IndexOf(",") > 0)
                                {
                                    valueKillSqlInjection = valueKillSqlInjection.ToLower();
                                }

                                result.Append(" And " + item.field + " IN (" + valueKillSqlInjection + ")");
                            }
                            /*
                             * Type: single int value
                             * Condition type: AND
                             */
                            else if (item.op == "=")
                            {
                                result.Append(" AND " + item.field + " = " + valueKillSqlInjection + "");
                            }

                            /*
                             * Type: single int value
                             * Condition type: AND
                             */
                            else if (item.op == "less")
                            {
                                result.Append(" AND " + item.field + " < " + valueKillSqlInjection);
                            }

                            /*
                             * Type: single value
                             * Condition type: AND
                             */
                            else if (item.op == "less_or_equal")
                            {
                                result.Append(" AND " + item.field + " <= " + "'" + valueKillSqlInjection + "'");
                            }

                            /*
                             * Type: single int value
                             * Condition type: AND
                             */
                            else if (item.op == "greater")
                            {
                                result.Append(" AND " + item.field + " > " + valueKillSqlInjection);
                            }

                            /*
                             * Type: single value
                             * Condition type: AND
                             */
                            else if (item.op == "greater_or_equal")
                            {
                                result.Append(" AND " + item.field + " >= " + "'" + valueKillSqlInjection + "'");
                            }

                            /*
                             * Type: from 2 or more date and time compare
                             * Condition type: AND
                             */
                            else if (item.op == "between")
                            {

                                var arrVl = valueKillSqlInjection.Split(':');
                                var vl1 = arrVl[0];
                                var vl2 = arrVl[1];
                                if (!string.IsNullOrEmpty(vl1))
                                {
                                    result.Append(" AND " + item.field + " >= CONVERT (DATETIME, '" + vl1 + "', 103)");
                                }

                                if (!string.IsNullOrEmpty(vl2))
                                {
                                    result.Append(" AND " + item.field + " <= CONVERT(DATETIME, '" + vl2 + "', 103)");
                                }
                            }


                            /*
                            * Type: searching multiple dates,remove time from dates
                            * Condition type: AND
                            */
                            else if (item.op == "and_date_equal")
                            {
                                if (valueKillSqlInjection.Contains(","))
                                {
                                    string[] dateValues = new string[1];
                                    dateValues = valueKillSqlInjection.Split(',');
                                    string query = string.Empty;
                                    for (int i = 0; i < dateValues.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(dateValues[i]))
                                        {
                                            if (i == 0)
                                            {
                                                query = " AND ( CAST(" + item.field + " AS DATE) = CAST('" + dateValues[i] + "' AS DATE)";
                                            }
                                            else
                                            {
                                                query = query + " OR CAST(" + item.field + " AS DATE) = CAST('" + dateValues[i] + "' AS DATE)";
                                            }
                                        }
                                    }
                                    query = query + ")";
                                    result.Append(query);
                                }
                                else
                                {
                                    result.Append(" AND CAST(" + item.field + " AS DATE) = CAST('" + valueKillSqlInjection + "' AS DATE)");
                                }
                            }

                            /*
                           * Type: searching date between from date and to date
                           * Condition type: AND
                           */
                            else if (item.op == "and_date_between")
                            {
                                if (valueKillSqlInjection.Contains("-"))
                                {
                                    string[] dateValues = new string[1];
                                    dateValues = valueKillSqlInjection.Split('-');
                                    string query = string.Empty;
                                    for (int i = 0; i < dateValues.Length; i++)
                                    {
                                        if (!string.IsNullOrEmpty(dateValues[i]))
                                        {
                                            if (i == 0)
                                            {
                                                query = " AND ( CAST(" + item.field + " AS DATE) >= CAST('" + DateTime.Parse(dateValues[i]).ToString("yyyy/MM/dd") + "' AS DATE)";
                                            }
                                            else
                                            {
                                                query = query + " AND CAST(" + item.field + " AS DATE) <= CAST('" + DateTime.Parse(dateValues[i]).ToString("yyyy/MM/dd") + "' AS DATE)";
                                            }
                                        }
                                    }
                                    query = query + ")";
                                    result.Append(query);
                                }
                                else
                                {
                                    result.Append(" AND CAST(" + item.field + " AS DATE) >= CAST('" + valueKillSqlInjection + "' AS DATE)");
                                }
                            }

                            else if (item.op == "not_equal_number")
                            {
                                int val;
                                bool isNumber = int.TryParse(item.value, out val);

                                if (isNumber)
                                {
                                    result.Append($" AND {item.field} <> {item.value}");
                                }
                            }

                            /*
                             * Group condition ex: AND (A.ABC = value OR A.BCD = value)
                             */
                            else if (item.op == "and_group_contains_with_or")
                            {
                                var fields = item.field.Split(',');
                                for (var i = 0; i < fields.Length; i++)
                                {
                                    if (i == 0)
                                        result.Append($" AND ( {fields[i]} LIKE N'%{valueKillSqlInjection}%' ");
                                    else if (i == (fields.Length - 1))
                                        result.Append($" OR {fields[i]} LIKE N'%{valueKillSqlInjection}%' )");
                                    else
                                        result.Append($" OR {fields[i]} LIKE N'%{valueKillSqlInjection}%' ");
                                }
                            }
                        }
                    }
                    return result.ToString();
                }
            }
        }
        public Int32 PageSize { get; set; }
        public Int32 type { get; set; }
        public Int32 PageIndex { get; set; }
        public Int64? USER_ID { get; set; }
        public Int32? USERIDCondiType { get; set; }
        public Int32 FromRecord
        {
            get
            {
                return (PageIndex - 1) * PageSize;
            }
        }
        /// <summary>
        ///Tienvv8: Check xem có condition mIN_WHERE or GetfilterRules
        /// </summary>
        public virtual bool HasCondition
        {
            get
            {
                if (!string.IsNullOrEmpty(mIN_WHERE) || (GetfilterRules != null && GetfilterRules.Count > 0))
                    return true;
                else
                    return false;
            }
        }
        public BaseCondition()
        {
            mIN_WHERE = string.Empty;
        }
        //public HttpPostedFileBase[] Files { get; set; }
    }
    public class BaseCondition<T> : BaseCondition
    {
        public T item { get; set; }
        public dynamic obj { get; set; }
        /// <summary>
        ///Tienvv8: Check xem có condition mIN_WHERE or GetfilterRules
        /// </summary>
        public override bool HasCondition
        {
            get
            {
                if (!string.IsNullOrEmpty(mIN_WHERE) || (GetfilterRules != null && GetfilterRules.Count > 0) || (item != null))
                    return true;
                else
                    return false;
            }
        }
        public BaseCondition() : base()
        {
        }
    }

    public class BasePostPara
    {
        public Int32 id { get; set; }
        public Int32 page { get; set; }
        public Int32 rows { get; set; }
        public string filterRules { get; set; }
        public string q { get; set; }
        public List<FilterItems> GetfilterRules
        {
            get
            {
                var result = new List<FilterItems>();
                try
                {
                    if (!string.IsNullOrEmpty(filterRules))
                    {
                        result = Libs.DeserializeObject<List<FilterItems>>(filterRules);
                    }
                    //return Libs.DeserializeObject<List<T>>(filterRules);
                }
                catch
                {
                    //return default(List<T>);
                }
                return result;
            }
        }
        public BasePostPara()
        {
        }
    }

    public class BasePostPara<T> : BasePostPara where T : new()
    {

        public List<T> ListItem { get; set; }

        //public Dictionary<string,dynamic> ListFilter { get; set; }

        public T item { get; set; }

        /*
         * Author: TuanVN4
         * Unit: FTEL - ISC
         * Created date: 17/07/2018
         * Purpose: To map all value from List<FilterItems> into an object
         */
        public T mapFilterToItem
        {
            get
            {
                var newItem = new T();
                foreach (var prop in GetfilterRules)
                {
                    var valueKillSqlInjection = Libs.KillSqlInjection(prop.value);
                    DataMapper.SetPropertyValue(newItem, prop.field, valueKillSqlInjection);
                }

                return newItem;
            }
        }
        /// <summary>
        /// Tienvv8: Convert to BaseCondition
        /// </summary>
        public BaseCondition<T> GetToBaseCondi()
        {
            return new BaseCondition<T>
            {
                PageIndex = this.page,
                PageSize = this.rows,
                GetfilterRules = this.GetfilterRules,
                item = this.item
            };
        }
    }

    public class FilterItems
    {
        public string field { get; set; }
        public string op { get; set; }
        public string value { get; set; }
    }
}
