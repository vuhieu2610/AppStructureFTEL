using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FTEL.Common.Utilities
{
    public class ValidateUtil
    {
        public static bool ValidatePassword(string password, out string errorMessage)
        {
            var input = password;
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Mật khẩu không nên để trống");
            }
            var meetsLengthRequirements = input.Length >= 2 && input.Length <= 30;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            //if (!hasLowerChar.IsMatch(input))
            //{
            //    errorMessage = "Mật khẩu phải chứa ít nhất một chữ thường";
            //    return false;
            //}
            //else if (!hasUpperChar.IsMatch(input))
            //{
            //    errorMessage = "Mật khẩu phải chứa ít nhất một chữ hoa";
            //    return false;
            //}
            if (!meetsLengthRequirements)
            {
                errorMessage = "Mật khẩu không được nhỏ hơn 2 ký tự và lớn hơn 30 ký tự";
                return false;
            }
            //if (!hasNumber.IsMatch(input))
            //{
            //    errorMessage = "Mật khẩu phải chứa ít nhất một giá trị số";
            //    return false;
            //}
            //else if (!hasSymbols.IsMatch(input))
            //{
            //    errorMessage = "Mật khẩu phải chứa ít nhất một ký tự trường hợp đặc biệt";
            //    return false;
            //}
            return true;
        }
        public static bool ValidateUsername(string userName, out string errorMessage)
        {
            var input = userName;
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("userName không nên để trống");
            }
            var meetsLengthRequirements = input.Length >= 10 && input.Length <= 50;
            var hasEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            var hasNumber = new Regex("^[0-9]+$");
            if (!meetsLengthRequirements)
            {
                errorMessage = "Tài khoản không được nhỏ hơn 10 ký tự và lớn hơn 50 ký tự";
                return false;
            }
            if (!hasEmail.IsMatch(input))
            {
                if (!hasNumber.IsMatch(input))
                {
                    errorMessage = "Tài khoản không đúng định dạng";
                    return false;
                }
                return true;
            }
            return true;
        }

        public static bool ValidateOtp(string otp, out string errorMessage)
        {
            var input = otp;
            errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("otp không nên để trống");
            }
            var meetsLengthRequirements = input.Length >= 2 && input.Length <= 10;
            var hasNumber = new Regex("^[0-9]*$");
            if (!meetsLengthRequirements)
            {
                errorMessage = "otp không được nhỏ hơn 2 ký tự và lớn hơn 10 ký tự";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                errorMessage = "otp không đúng định dạng";
                return false;
            }
            else
            {
                return true;
            }
        }
        public static string ValidateStringCode(string objCode)
        {
            var input = "";
            try
            {
                input = objCode.ToUpper().Trim();
                var meetsLengthRequirements = input.Length >= 2 && input.Length <= 50;
                var hasString = new Regex("^[A-Z0-9_-]+$");
                if (!meetsLengthRequirements)
                {
                    input = "";
                }
                else if (!hasString.IsMatch(input))
                {
                    input = "";
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return input;
        }

        /// <summary>
        /// Kiểm tra định dạng của email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>True: Email đúng định dạng; False: Email sai định dạng</returns>
        public static bool ValidateEmail(string email)
        {
            try
            {
                // Kiểm tra email trống
                if (string.IsNullOrWhiteSpace(email))
                {
                    return false;
                }

                // Kiểm tra định dạng của email
                var hasEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                if (!hasEmail.IsMatch(email.Trim()))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }

    public class Validator : ValidationAttribute
    {
        public static IEnumerable<string> Validate(object o)
        {
            var type = o.GetType();
            var properties = type.GetProperties();
            var attrType = typeof(ValidationAttribute);
            return from propertyInfo in properties let customAttributes = propertyInfo.GetCustomAttributes(attrType, true) from customAttribute in customAttributes let validationAttribute = (ValidationAttribute)customAttribute let isValid = validationAttribute.IsValid(propertyInfo.GetValue(o, BindingFlags.GetProperty, null, null, null)) where !isValid select validationAttribute.ErrorMessage;
            //foreach (var propertyInfo in properties)
            //{
            //    var customAttributes = propertyInfo.GetCustomAttributes(attrType, inherit: true);

            //    foreach (var customAttribute in customAttributes)
            //    {
            //        var validationAttribute = (ValidationAttribute)customAttribute;

            //        var isValid = validationAttribute.IsValid(propertyInfo.GetValue(o, BindingFlags.GetProperty, null, null, null));

            //        if (!isValid)
            //        {
            //            yield return validationAttribute.ErrorMessage;
            //        }
            //    }
            //}

        }
        public static IEnumerable<string> ValidateLinq(object o)
        {
            return TypeDescriptor
                .GetProperties(o.GetType())
                .Cast<PropertyDescriptor>()
                .SelectMany(pd => pd.Attributes.OfType<ValidationAttribute>()
                                    .Where(va => !va.IsValid(pd.GetValue(o))))
                                    .Select(xx => xx.ErrorMessage);
        }
    }
}
