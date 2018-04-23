# How to connect to a remote data service from a Silverlight application


<p><strong>Scenario:</strong><strong><br /> </strong><br /> In this example we will create a <strong>WCF IDataStore</strong> service that will be used by our client (SilverLight Application) as a data layer. Instead of direct connection to the database, our client will connect to a remote service, which is way more secure and thus important in many enterprise scenarios as database connection settings are not exposed to the client. Also all inquiries will be performed <strong>asynchronously</strong> to not block the UI thread.</p>
<p><strong>Steps to implement:</strong></p>
<p><strong>1.</strong> Implement the <strong>WCF service </strong>as shown in the <a href="https://www.devexpress.com/Support/Center/p/E4930">How to connect to a remote data service instead of using a direct database connection</a> example.</p>
<p><strong>2.</strong> Add a new Silverlight Application project.</p>
<p><strong>3. </strong>Reference<strong> DevExpress.Data</strong> and <strong>DevExpress.Xpo</strong> assemblies.</p>
<p><strong>4.</strong> Implement a <strong>Customer</strong> class as shown in the example's <em>Customer.xx</em> file.</p>
<p><strong>5.</strong> Drop <a href="https://www.devexpress.com/Products/NET/Controls/Silverlight/Grid/"><u>GridConrtol</u></a> to <em>MainPage</em>.</p>
<p><strong>6.</strong> Modify the <em>MainPage.xx</em> file as shown in the example's <em>MainPage.cs</em> file.<br /> The MainPage uses <a href="http://msdn.microsoft.com/en-us/library/system.windows.threading.dispatcher(v=vs.110).aspx"><u>Dispatcher</u></a> to call the <strong>BeginInitializeDataSource</strong> method asynchronously, which creates a query and uses the <a href="http://documentation.devexpress.com/#XPO/DevExpressXpoXPQueryExtensions_EnumerateAsync%5bT%5dtopic"><u>EnumerateAsync</u></a> method to set the GridControl data source.</p>
<p>As a result, you will see the following page:<br /> <img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-connect-to-a-remote-data-service-from-a-silverlight-application-e4993/13.1.9+/media/c369ed27-378f-4f26-a059-2acde6727602.png"></p>
<p><strong>Important notes:</strong><br /> At the development stage you can face the <strong>CommunicationException</strong>. This could be due to the attempt to access a service in a <strong>cross-domain way</strong> without a proper cross-domain policy in place, or a policy that is unsuitable for <a href="http://msdn.microsoft.com/en-us/library/ff512435.aspx"><u>SOAP</u></a> services. You will need to publish a <a href="http://msdn.microsoft.com/ru-ru/library/cc838250(v=vs.95).aspx"><u>cross-domain policy file and ensure that it allows SOAP-related HTTP headers to be sent</u></a>. Also you can simply add your Silverlight application via the "Silverlight Application" window in the WCFService property window as shown in the following screenshot:<br /> <img src="https://raw.githubusercontent.com/DevExpress-Examples/how-to-connect-to-a-remote-data-service-from-a-silverlight-application-e4993/13.1.9+/media/685adfde-c292-441e-bdb4-706792675397.png"></p>
<p> </p>
<p><strong>Troubleshooting</strong><br />1. If WCF throws the "<em>Entity is too large</em>" error, you can apply a standard solution from StackOverFlow: <a href="http://stackoverflow.com/questions/10122957/">http://stackoverflow.com/questions/10122957/</a><br />2. If WCF throws the "<em>The maximum string content length quota (8192) has been exceeded while reading XML data.</em>" error, you can extend bindings in the following manner:</p>
<pre class="cr-code">[XML]<code><bindings>
      <basicHttpBinding>
        <binding name="ServicesBinding" maxBufferPoolSize="2147483647" maxReceivedMessageSize="2147483647" maxBufferSize="2147483647" transferMode="Streamed" >
          <readerQuotas maxDepth="2147483647"
            maxArrayLength="2147483647"
            maxStringContentLength="2147483647"/>
        </binding>
      </basicHttpBinding>
</bindings></code></pre>
<p> </p>
<p>See <a href="http://stackoverflow.com/questions/6600057/the-maximum-string-content-length-quota-8192-has-been-exceeded-while-reading-x">The maximum string content length quota (8192) has been exceeded while reading XML data</a></p>

<br/>


