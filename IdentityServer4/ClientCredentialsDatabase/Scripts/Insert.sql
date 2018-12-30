INSERT INTO Resources([Name], [DisplayName], [Description], [Enabled]) VALUES ('apiTest', 'API Test','API Test',1)
INSERT INTO Clients VALUES('test')
INSERT INTO Secrets VALUES('123')
INSERT INTO GrantTypes VALUES('client_credentials')
INSERT INTO Scopes([Name], [DisplayName], [Description], [Required], [Emphasize],[ShowInDiscoveryDocument]) VALUES('apiTest','API Test', 'API Test',0,0,1)

GO

INSERT INTO ClientGrantTypes ([Client_id], [GrantType_id]) VALUES ('test', 1)
INSERT INTO ClientScopes ([Client_id], [Scope_id]) VALUES ('test', 1)
INSERT INTO ClientSecrets ([Client_id] ,[Secret_id]) VALUES ('test', '123')
INSERT INTO ResourceScopes ([Resource_id] ,[Scope_id]) VALUES (1,1)
GO