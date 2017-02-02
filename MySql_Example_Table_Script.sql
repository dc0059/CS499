CREATE TABLE `users_log` (
  `UserID` bigint(20) DEFAULT NULL,
  `UserName` varchar(20) DEFAULT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `EmailAddress` varchar(50) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) DEFAULT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) DEFAULT NULL,
  `Version` int(11) DEFAULT NULL,
  `ModifiedStatus` varchar(1) DEFAULT NULL,
  `DeletedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `DeletedBy` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `users` (
  `UserID` bigint(20) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(20) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `EmailAddress` varchar(50) NOT NULL,
  `IsActive` tinyint(1) NOT NULL DEFAULT '1',
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL,
  `Version` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`UserID`),
  UNIQUE KEY `username_unique` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=utf8;

DROP TRIGGER IF EXISTS lkr_dev.users_bd;

CREATE TRIGGER `lkr_dev`.`users_bd`
   BEFORE DELETE
   ON lkr_dev.users
   FOR EACH ROW
BEGIN
   INSERT INTO users_log(UserID,
                         UserName,
                         FirstName,
                         LastName,
                         EmailAddress,
                         IsActive,
                         CreatedDate,
                         CreatedBy,
                         LastModifiedDate,
                         LastModifiedBy,
                         Version,
                         ModifiedStatus,
                         DeletedDate)
   VALUES (OLD.UserID,
           OLD.UserName,
           OLD.FirstName,
           OLD.LastName,
           OLD.EmailAddress,
           OLD.IsActive,
           OLD.CreatedDate,
           OLD.CreatedBy,
           OLD.LastModifiedDate,
           OLD.LastModifiedBy,
           OLD.Version,
           'D',
           CURRENT_TIMESTAMP);
END;

DROP TRIGGER IF EXISTS lkr_dev.users_bi;

CREATE TRIGGER `lkr_dev`.`users_bi`
   BEFORE INSERT
   ON lkr_dev.users
   FOR EACH ROW
BEGIN
   SET NEW.CREATEDDATE = CURRENT_TIMESTAMP;
   SET NEW.VERSION = 1;
END;

DROP TRIGGER IF EXISTS lkr_dev.users_bu;

CREATE TRIGGER `lkr_dev`.`users_bu`
   BEFORE UPDATE
   ON lkr_dev.users
   FOR EACH ROW
BEGIN
   SET NEW.VERSION = OLD.VERSION + 1;
   SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP;

   INSERT INTO users_log(UserID,
                         UserName,
                         FirstName,
                         LastName,
                         EmailAddress,
                         IsActive,
                         CreatedDate,
                         CreatedBy,
                         LastModifiedDate,
                         LastModifiedBy,
                         Version,
                         ModifiedStatus)
   VALUES (OLD.UserID,
           OLD.UserName,
           OLD.FirstName,
           OLD.LastName,
           OLD.EmailAddress,
           OLD.IsActive,
           OLD.CreatedDate,
           OLD.CreatedBy,
           OLD.LastModifiedDate,
           OLD.LastModifiedBy,
           OLD.Version,
           'U');
END;