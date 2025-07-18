using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace TSmartClinic.Core.Domain.Helpers
{
    public static class Helper
    {
        public static void RemoverEspacosEmBranco<T>(this T dto) where T : class
        {
            if (dto == null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            PropertyInfo[] properties = dto.GetType().GetProperties();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    string value = (string)property.GetValue(dto);

                    if (value != null)
                    {
                        string newValue = value.Trim();
                        property.SetValue(dto, newValue);
                    }
                }
            }
        }
        public static string RemoverAcentos(this string texto)
        {
            string textoSemAcentos = texto.Normalize(NormalizationForm.FormD);
            Regex regex = new Regex("[^a-zA-Z0-9 ]");

            return regex.Replace(textoSemAcentos, "");
        }

        public static bool CnpjIsValid(this string cnpj)
        {
            cnpj = Regex.Replace(cnpj, @"[^\d]", "");

            if (cnpj.Length != 14)
                return false;

            if (new string(cnpj[0], cnpj.Length) == cnpj)
                return false;

            int[] multiplicadores1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool CpfIsValid(this string cpf)
        {
            cpf = Regex.Replace(cpf, @"[^\d]", "");

            if (cpf.Length != 11)
                return false;

            if (new string(cpf[0], cpf.Length) == cpf)
                return false;

            int[] multiplicadores1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static string FormatarCpf(this string cpf)
        {
            cpf = Regex.Replace(cpf, @"\D", "");

            if (cpf.Length != 11)
            {
                throw new ArgumentException("CPF deve ter 11 dígitos.");
            }

            return Regex.Replace(cpf, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
        }

        public static string FormatarCnpj(this string cnpj)
        {
            cnpj = Regex.Replace(cnpj, @"\D", "");

            if (cnpj.Length != 14)
            {
                throw new ArgumentException("CNPJ deve ter 14 dígitos.");
            }

            return Regex.Replace(cnpj, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", "$1.$2.$3/$4-$5");
        }

    }
}
