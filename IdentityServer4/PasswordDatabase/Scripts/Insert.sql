INSERT INTO Resources([Name], [DisplayName], [Description], [Enabled]) VALUES ('apiTest', 'API Test','API Test',1)
INSERT INTO Clients VALUES('test','test')
INSERT INTO Secrets VALUES('123','123')
INSERT INTO GrantTypes VALUES('password')
INSERT INTO Scopes([Name], [DisplayName], [Description], [Required], [Emphasize],[ShowInDiscoveryDocument]) VALUES('apiTest','API Test', 'API Test',0,0,1)

GO

INSERT INTO ClientGrantTypes ([Client_id], [GrantType_id]) VALUES ('test', 1)
INSERT INTO ClientScopes ([Client_id], [Scope_id]) VALUES ('test', 1)
INSERT INTO ClientSecrets ([Client_id] ,[Secret_id]) VALUES ('test', '123')
INSERT INTO ResourceScopes ([Resource_id] ,[Scope_id]) VALUES (1,1)
GO

INSERT INTO Users (Id, IsActive, UserName, [Password]) VALUES (NEWID(), 1, 'test', 'E10ADC3949BA59ABBE56E057F20F883E')
GO