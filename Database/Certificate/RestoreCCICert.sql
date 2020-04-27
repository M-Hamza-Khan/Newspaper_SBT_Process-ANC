CREATE CERTIFICATE CCIServerCert  
    FROM FILE = N'D:\Projects\VT\CCI\TDE\CCIServerCert.cert'   
    WITH PRIVATE KEY (FILE = N'D:\Projects\VT\CCI\TDE\CCIServerCert.prvk',   
    DECRYPTION BY PASSWORD = 'CC!prvk'); 
GO