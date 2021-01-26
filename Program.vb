Imports System.Net
Imports System.IO
Imports System.Security
Imports System.Net.Security

Module Program
    Sub Main(args As String())
        Console.WriteLine("Hello World!")

        webRequest("https://27.79.223.91:8089/api",
                                "action=challenge&user=cdrapi&version=1.0",
                                "application/x-www-form-urlencoded",
                                "application/x-www-form-urlencoded",
                                "POST")


        Console.ReadLine()
    End Sub

    Private Function webRequest(ByVal pzUrl As String,
                                ByVal pzData As String,
                                ByVal Optional ContentType As String = "application/json",
                                ByVal Optional Accept As String = "application/json",
                                ByVal Optional Method As String = "POST",
                                ByVal Optional pzAuthorization As String = "",
                                ByVal Optional functionName As String = "") As String
        '//string pzUrl = "http://192.168.0.10/Invoice/serverAPI.aspx";
        '//string pzAuthorization; , string clientId, string username, string password

        Console.WriteLine(String.Format("Time {7}; pzUrl: {0}; pzData: {1}; ContentType: {2}; Accept: {3}; Method: {4}; pzAuthorization: {5}; functionName: {6}",
                                        pzUrl, pzData, ContentType, Accept, Method, pzAuthorization, functionName, DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")))
        Dim result As String = String.Empty
        Try
            Dim _HttpWebRequest As HttpWebRequest
            '//pzAuthorization = Base64Encode ("KVP^" + clientId + "^" + username + "^" + password + "");
            _HttpWebRequest = System.Net.WebRequest.Create(pzUrl)
            _HttpWebRequest.ContentType = ContentType
            _HttpWebRequest.Accept = Accept
            _HttpWebRequest.Method = Method
            If Not String.IsNullOrEmpty(pzAuthorization) Then _HttpWebRequest.Headers.Add("Authorization", pzAuthorization)
            If Not String.IsNullOrEmpty(functionName) Then _HttpWebRequest.Headers.Add("FunctionName", functionName)

            If (Not String.IsNullOrEmpty(pzData)) Then
                Dim streamWriter = New StreamWriter(_HttpWebRequest.GetRequestStream())
                streamWriter.Write(pzData)
                streamWriter.Flush()
                streamWriter.Close()
            End If
            InitiateSSLTrust()
            Dim _httpResponse = _HttpWebRequest.GetResponse()
            Dim streamReader = New StreamReader(_httpResponse.GetResponseStream())
            result = streamReader.ReadToEnd()
            String.Format("Time {8}; pzUrl: {0}; pzData: {1}; ContentType: {2}; Accept: {3}; Method: {4}; pzAuthorization: {5}; functionName: {6}; result: {7}",
                                        pzUrl, pzData, ContentType, Accept, Method, pzAuthorization, functionName, result, DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")).ConsoleGreen()
        Catch Ex As Exception
            String.Format("Time {8}; pzUrl: {0}; pzData: {1}; ContentType: {2}; Accept: {3}; Method: {4}; pzAuthorization: {5}; functionName: {6}; Exception: {7}",
                                        pzUrl, pzData, ContentType, Accept, Method, pzAuthorization, functionName, Ex.ToString(), DateTime.UtcNow.ToString("dd/MM/yyyy HH:mm:ss")).ConsoleRed()
        End Try
        Return result
    End Function

    Private Sub InitiateSSLTrust()
        '// ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AcceptAllCertifications)
        ServicePointManager.ServerCertificateValidationCallback =
          Function(se As Object,
          cert As Cryptography.X509Certificates.X509Certificate,
          chain As Cryptography.X509Certificates.X509Chain,
          sslerror As SslPolicyErrors) True
    End Sub
End Module
