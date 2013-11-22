Subby
================================
A Windows console application for replacing variables in one file from another.

Author
------------------------
Lam Chan @lamchakchan

http://www.codelyfe.com

Command Line Usages
------------------------
<pre>
    subby -f json -s sourcevariablefile -t targetreplacementfile -d savefile
    subby -s source.json -t web.template.config -d web.config
    subby -s source1.json,source2.xml -t web.template.config -d web.config
    subby -s ../source1.json -t ./templates/web.config -d web.config
</pre>

Example JSON Formatted Variable File
------------------------
```json
{    
	'@environmentSettings.type' : 'QA',
	'@imageUploadSettings.jobIntervalSeconds' : '120',
	'@emaliNotificationSettings.smtpServer' : 'smtp.gmail.com',
	'@emaliNotificationSettings.smtpPort' : '443',
	'@emaliNotificationSettings.toEmail' : 'user1@foo.com; user2@bar.com',
	'@emailNotificationSettings.fromEmail' : 'systems@hellworld.com',
	'@awsSettings.accesskey' : 'testKey',
	'@awsSettings.secretKey' : 'D+secretKey',
	'@awsSettings.bucket' : 'publicBucket',
}
```

Example Target File for Value Substitution
------------------------
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="environmentSettings" type="Fake.Core.Configurations.EnvironmentSettings" />
    <section name="imageUploadSettings" type="Fake.Core.Configurations.ImageUploadSettings" />
    <section name="awsSettings" type="Fake.Core.Configurations.AWSSettings" />
    <section name="emaliNotificationSettings" type="Fake.Core.Configurations.EmailNotificationSettings" />
  </configSections>
  <connectionStrings>
    <add name="ImagesDB" connectionString="Data Source=dvdb01c;Initial Catalog=ImageDB;User ID=ImageCloudPushService; Password=6Cx7VƒbjRqrvAhER" />
  </connectionStrings>
  <environmentSettings type="@environmentSettings.type" />
  <imageUploadSettings jobIntervalSeconds="@imageUploadSettings.jobIntervalSeconds" />
  <awsSettings accessKey="@awsSettings.accesskey" secretKey="@awsSettings.secretKey" bucket="@awsSettings.bucket" />
  <emaliNotificationSettings smtp="@emaliNotificationSettings.smtpServer" smtpPort="@emaliNotificationSettings.smtpPort" toEmail="@emaliNotificationSettings.toEmail" fromEmail="@emailNotificationSettings.fromEmail" />
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.5" />
  </startup>
  <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <dependentAssembly>
        <assemblyIdentity name="RazorEngine" publicKeyToken="9ee697374c7e744a" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-3.4.0.0" newVersion="3.4.0.0" />
      </dependentAssembly>
      <dependentAssembly>
        <assemblyIdentity name="NLog" publicKeyToken="5120e14c03d0593c" culture="neutral" />
        <bindingRedirect oldVersion="0.0.0.0-2.1.0.0" newVersion="2.1.0.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>
</configuration>
```


TODO's
------------------------

- [ ] implement XML Parser
- [ ] implement New Line Text Parser
- [ ] implement target context for HTTP resources
- [ ] implement variable context for HTTP resources 

 
