namespace SchoolManagmentSystem.Data.Commons
{
    public class GeneralLocalizableEntities
    {
        public string GetGeneralLocalizedEntity(string textAr, string textEn)
        {
            var culuture = Thread.CurrentThread.CurrentCulture;
            if (culuture.TwoLetterISOLanguageName.ToLower() == "ar")
            {
                return textAr;
            }
            else
            {
                return textEn;
            }
        }
    }
}
