namespace Factory.Entension{
    public static class Mapper{
        public static UserInfo ToUserInfo(this DynamicFormModel dynamicForm)
        {
            var dynamicFields = dynamicForm.Fields;
                return new UserInfo
                {
                    LastName = dynamicFields.ContainsKey("LastName") ? dynamicFields["LastName"].ToString() : null,
                    FirstName = dynamicFields.ContainsKey("FirstName")?dynamicFields["FirstName"].ToString(): null,
                    Gender = dynamicFields.ContainsKey("Gender")? dynamicFields["Gender"].ToString(): null,
                    Age =dynamicFields.ContainsKey("Age")? int.Parse(dynamicFields["Age"].ToString()): 0,
                    Email = dynamicFields.ContainsKey("Email")? dynamicFields["Email"].ToString(): null,
                    PhoneNumber =dynamicFields.ContainsKey("PhoneNumber")? dynamicFields["PhoneNumber"].ToString(): null,
                    SSN =dynamicFields.ContainsKey("SSN")? dynamicFields["SSN"].ToString():null,
                    StreetAddress = dynamicFields.ContainsKey("StreetAddress")?dynamicFields["StreetAddress"].ToString():null,
                    City =dynamicFields.ContainsKey("City")? dynamicFields["City"].ToString():null,
                    State = dynamicFields.ContainsKey("State")?dynamicFields["State"].ToString():null,
                    Password =dynamicFields.ContainsKey("Password")? dynamicFields["Password"].ToString():null,
                    MiddleName = dynamicFields.ContainsKey("MiddleName") ? dynamicFields["MiddleName"].ToString() : null,
                    ZipCode =dynamicFields.ContainsKey("ZipCode")? dynamicFields["ZipCode"].ToString():null,
                    Country =dynamicFields.ContainsKey("Country")? dynamicFields["Country"].ToString():null,
                    DateOfBirth =dynamicFields.ContainsKey("DateOfBirth")? dynamicFields["DateOfBirth"].ToString():null,
                    LanguagePreference =dynamicFields.ContainsKey("LanguagePreference")? dynamicFields["LanguagePreference"].ToString():null,
                    ThemePreference =dynamicFields.ContainsKey("ThemePreference")? dynamicFields["ThemePreference"].ToString():null,
                    SecurityQuestion =dynamicFields.ContainsKey("SecurityQuestion")? dynamicFields["SecurityQuestion"].ToString():null,
                    SecurityAnswer =dynamicFields.ContainsKey("SecurityAnswer")? dynamicFields["SecurityAnswer"].ToString():null,
                    OrganizationName =dynamicFields.ContainsKey("OrganizationName")? dynamicFields["OrganizationName"].ToString():null,
                    OrganizationAddress =dynamicFields.ContainsKey("OrganizationAddress")? dynamicFields["OrganizationAddress"].ToString():null,
                    CEO =dynamicFields.ContainsKey("CEO")? dynamicFields["CEO"].ToString():null,
                    CeoPhonenumber = dynamicFields.ContainsKey("CeoPhonenumber")? dynamicFields["CeoPhonenumber"].ToString():null,
                    HROffice = dynamicFields.ContainsKey("HROffice")? dynamicFields["HROffice"].ToString():null,
                    HrName = dynamicFields.ContainsKey("HrName")? dynamicFields["HrName"].ToString():null,
                    Transportsystem = dynamicFields.ContainsKey("Transportsystem")? dynamicFields["Transportsystem"].ToString():null,

            };
           
        }
    }
}