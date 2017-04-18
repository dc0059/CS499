CREATE DATABASE  IF NOT EXISTS `cs_499_tcms` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `cs_499_tcms`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: cs_499_tcms
-- ------------------------------------------------------
-- Server version	5.7.17-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `user_log`
--

DROP TABLE IF EXISTS `user_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `user_log` (
  `EmployeeID` int(10) DEFAULT NULL,
  `UserName` varchar(20) DEFAULT NULL,
  `FirstName` varchar(20) DEFAULT NULL,
  `MiddleName` varchar(20) DEFAULT NULL,
  `LastName` varchar(20) DEFAULT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `City` varchar(50) DEFAULT NULL,
  `State` varchar(20) DEFAULT NULL,
  `ZipCode` int(5) DEFAULT NULL,
  `HomePhone` varchar(10) DEFAULT NULL,
  `CellPhone` varchar(10) DEFAULT NULL,
  `EmailAddress` varchar(100) DEFAULT NULL,
  `PayRate` double DEFAULT NULL,
  `EmploymentDate` date DEFAULT NULL,
  `AccessLevel` int(11) DEFAULT NULL,
  `HomeStore` varchar(150) DEFAULT NULL,
  `JobDescription` varchar(20) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT NULL,
  `HashKey` varchar(2000) DEFAULT NULL,
  `Passphrase` varchar(2000) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) DEFAULT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) DEFAULT NULL,
  `Version` int(11) DEFAULT NULL,
  `ModifiedStatus` varchar(1) DEFAULT NULL,
  `DeletedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `DeletedBy` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user_log`
--

LOCK TABLES `user_log` WRITE;
/*!40000 ALTER TABLE `user_log` DISABLE KEYS */;
INSERT INTO `user_log` VALUES (1,'dc0059','Donal','David','Cavanaugh','12345 Some Where','Huntsville','Alabama',35805,'1234567891','1234567891','dc0059@uah.edu',50,'2017-03-25',0,'Uah','The man',1,'','','2017-03-25 20:30:39','dc0059','0000-00-00 00:00:00','dc0059',1,'U','0000-00-00 00:00:00',NULL),(1,'dc0059','Donal','David','Cavanaugh','12345 Some Where','Huntsville','Alabama',35805,'1234567891','1234567891','dc0059@uah.edu',50,'2017-03-25',0,'Uah','The man',1,'','6p8Kd/XS5+qnZ+NBy+bNU601KAuuRyTzsFKKPqKlSKLfIlErc5GInBXfF9V0YMRkZiK0lc/Fja+xA+kN4nTz8a4SKwPxjEdV55PXBvNW4rJJoqp4sNVcRBPEJET0+tMnvo40Qdv9cOMoY6RIG60XosPXEk8IGJHkvoat5PHxgrxAPcFBzFmDu996TIQJfjMsUfm7uSFYqM97lzaKFYoGqiARujtfrptSODbKU8eL71DmVSEI9HdfaLDRlvXmHxILs81u46waAOB4MxbRbjViKLBwvDzOMCrUVW0n7IfzgeZLg6Uw2uaWgmm2A3+2T0uYH5WWcqRsJVIJXOpnAuDr7w==','2017-03-25 20:30:39','dc0059','2017-03-25 20:35:05','dc0059',2,'U','0000-00-00 00:00:00',NULL),(1,'dc0059','Donal','David','Cavanaugh','12345 Some Where','Huntsville','Alabama',35805,'1234567891','1234567891','dc0059@uah.edu',50,'2017-03-25',0,'Uah','The man',1,'+lP5JuXIeVc5kSD6GU6lp5tbY6NYUQ7V3VbsuCCf/oPh2uL2oB5Ii3F6894UvUxXV+4ByvjPxAuBWfCvsKnd/A==','6p8Kd/XS5+qnZ+NBy+bNU601KAuuRyTzsFKKPqKlSKLfIlErc5GInBXfF9V0YMRkZiK0lc/Fja+xA+kN4nTz8a4SKwPxjEdV55PXBvNW4rJJoqp4sNVcRBPEJET0+tMnvo40Qdv9cOMoY6RIG60XosPXEk8IGJHkvoat5PHxgrxAPcFBzFmDu996TIQJfjMsUfm7uSFYqM97lzaKFYoGqiARujtfrptSODbKU8eL71DmVSEI9HdfaLDRlvXmHxILs81u46waAOB4MxbRbjViKLBwvDzOMCrUVW0n7IfzgeZLg6Uw2uaWgmm2A3+2T0uYH5WWcqRsJVIJXOpnAuDr7w==','2017-03-25 20:30:39','dc0059','2017-03-25 20:35:35','dc0059',3,'U','0000-00-00 00:00:00',NULL),(1,'dc0059','Donal','David','Cavanaugh','12345 Some Where','Huntsville','Alabama',35805,'1234567891','1234567891','dc0059@uah.edu',50,'2017-03-25',0,'Uah','The man',1,'+lP5JuXIeVc5kSD6GU6lp5tbY6NYUQ7V3VbsuCCf/oPh2uL2oB5Ii3F6894UvUxXV+4ByvjPxAuBWfCvsKnd/A==','+0bvFRehccWBw6it1KjAWuHHlFeWJqS8fQ1aL+Pq9ffdt1HeVUoZtBsIxT/Qxyv+x4FflbRNyRNwaPZLhSowm9Vqv8rwdmP1pQFSMITAdF5156w3w+5k+PVTNkXtHUVJ391kICUa4FUhuEAYu/NmwXmljccCVhboGTmerCAxXw97wfu1VjSuev6OGFjj/JWpvfljYK1E4wfmJU3oIPlEOBQeHnob4TmHW1Ta+sHGUACqY1fjGoN0wbZSMVDcJWtQ2/qnkoTUv7GMmoy1aozHh09+Oa7RH2SyKvEnLQ/a0B24ztkaYS3D89lt+TAR8VmB6MTiNrXIvbM7tJ1LNPTrzg==','2017-03-25 20:30:39','dc0059','2017-03-25 21:43:15','dc0059',4,'U','0000-00-00 00:00:00',NULL),(2,'ztj0002','Zach','Taylor','Johnson','4545 Over There','Guntersville','AL',35976,'7063156775','7063156775','ztj0002@uah.edu',45,'2012-06-18',1,'Store A','Technician',1,'stuff','other stuff','2017-03-26 17:34:38','cs_499_tcms','0000-00-00 00:00:00','cs_499_tcms',1,'D','2017-03-26 17:39:48','cs_499_tcms'),(1,'dc0059','Donal','David','Cavanaugh','12345 Some Where','Huntsville','Alabama',35805,'1234567891','1234567891','dc0059@uah.edu',50,'2017-03-25',0,'Uah','The man',1,'F6NHdfGcZn2Qgm3rhFBxTngbX6NuRSzPMUbWrSiAq9jiSTkw7Pdg9KSwWjmp64HA+zsmo0cbelrvBVBXKrVwkg==','+0bvFRehccWBw6it1KjAWuHHlFeWJqS8fQ1aL+Pq9ffdt1HeVUoZtBsIxT/Qxyv+x4FflbRNyRNwaPZLhSowm9Vqv8rwdmP1pQFSMITAdF5156w3w+5k+PVTNkXtHUVJ391kICUa4FUhuEAYu/NmwXmljccCVhboGTmerCAxXw97wfu1VjSuev6OGFjj/JWpvfljYK1E4wfmJU3oIPlEOBQeHnob4TmHW1Ta+sHGUACqY1fjGoN0wbZSMVDcJWtQ2/qnkoTUv7GMmoy1aozHh09+Oa7RH2SyKvEnLQ/a0B24ztkaYS3D89lt+TAR8VmB6MTiNrXIvbM7tJ1LNPTrzg==','2017-03-25 20:30:39','dc0059','2017-03-25 21:44:24','dc0059',5,'U','0000-00-00 00:00:00',NULL),(1,'Johnsza','Zach','Taylor','Johnson','495 Trevor Lane','Macon','GA',31201,'7063156775','7063156775','johnsza@gmail.com',45,'2012-06-18',2,'Store A','Bro',0,'stuff','other stuff','2017-03-25 20:30:39','dc0059','2017-03-26 17:41:03','cs_499_tcms',6,'U','0000-00-00 00:00:00',NULL),(1,'Johnsza','Zach','Taylor','Johnson','495 Trevor Lane','Macon','GA',31201,'7063156775','7063156775','johnsza@gmail.com',45,'2012-06-18',2,'Store A','Bro',0,'stuff','other stuff','2017-03-25 20:30:39','dc0059','2017-03-26 18:28:09','cs_499_tcms',7,'U','0000-00-00 00:00:00',NULL),(1,'Johnsza','Zach','Taylor','Johnson','495 Trevor Lane','Macon','GA',31201,'7063156775','7063156775','johnsza@gmail.com',45,'2012-06-18',2,'Store A','Bro',0,'stuff','other stuff','2017-03-25 20:30:39','dc0059','2017-03-26 18:32:14','cs_499_tcms',8,'U','0000-00-00 00:00:00',NULL),(1,'dc0059','Donal','David','Cavanaugh','12345 Some Where','Huntsville','Alabama',35805,'1234567891','1234567891','dc0059@uah.edu',50,'2017-03-25',0,'Uah','The man',1,'+lP5JuXIeVc5kSD6GU6lp5tbY6NYUQ7V3VbsuCCf/oPh2uL2oB5Ii3F6894UvUxXV+4ByvjPxAuBWfCvsKnd/A==','6p8Kd/XS5+qnZ+NBy+bNU601KAuuRyTzsFKKPqKlSKLfIlErc5GInBXfF9V0YMRkZiK0lc/Fja+xA+kN4nTz8a4SKwPxjEdV55PXBvNW4rJJoqp4sNVcRBPEJET0+tMnvo40Qdv9cOMoY6RIG60XosPXEk8IGJHkvoat5PHxgrxAPcFBzFmDu996TIQJfjMsUfm7uSFYqM97lzaKFYoGqiARujtfrptSODbKU8eL71DmVSEI9HdfaLDRlvXmHxILs81u46waAOB4MxbRbjViKLBwvDzOMCrUVW0n7IfzgeZLg6Uw2uaWgmm2A3+2T0uYH5WWcqRsJVIJXOpnAuDr7w==','2017-03-25 20:30:39','dc0059','2017-03-27 22:18:23','dc0059',9,'U','0000-00-00 00:00:00',NULL),(4,'driver','John','Snow','Ded','12265','Big Castle','The North',12340,'1234567890','1234567890','b.ded@GoT.com',100,'2017-04-03',3,'UAH','Driver',1,'7m631lIc1ZVdBc/UaNbhDPBJnTM0KyHhw4rVW7+h+bxYu4fyNc6trOrkR9HcOT6E7MaWM7MOPemvsdFJWi1fxw==','YzoO3T9af3BIGg+dNuozBtg0SFOa3P/iHSNmC0sO+rMfB9W1OokZJhQjlUlpe7Mdcks+0Gw/r6SPekH0ve9O9UlogIzZdDL85c3YC34iuvKKQuCRk48rxIabemc60dLEsNrBXKqlZXuMVC76+7kXa1zZhfQ8xqLKutXcplwr/3tQaF5t71A499UbJ6aY6oj0wQvjc18s0i/PIGGxjbEpJBlMD7SMBziYyTl8l97l5RlL/Pz8VInPyy18NJtf6eq+mn7mcjCdhoaClp/VQreECoyNbFZd4qY0qfSF/XRtXr8z9xs/SG6R49Dw+4itq3mzdJc+7JDCL6JZK7l/MzQfCg==','2017-04-03 22:27:31','dc0059','0000-00-00 00:00:00','dc0059',1,'U','0000-00-00 00:00:00',NULL),(3,'ztj0002','Zach','Taylor','Johnson','4545 Over There','Guntersville','AL',35976,'7063156775','7063156775','ztj0002@uah.edu',45,'2012-06-18',1,'Store A','Technician',1,'stuff','other stuff','2017-03-26 18:28:09','cs_499_tcms','0000-00-00 00:00:00','cs_499_tcms',1,'U','0000-00-00 00:00:00',NULL),(3,'ztj0002','Zach','Taylor','Johnson','4545 Over There','Guntersville','AL',35976,'7063156775','7063156775','ztj0002@uah.edu',45,'2012-06-18',1,'Store A','Technician',1,'/yiRAdYrAb0UcwHmvzhmBoRdiEJunKpVEx7PGRyUlQxVDPyy6AzuAgtBvxQ4ZSJEySalH3A0gEVbrPei2qBq2A==','other stuff','2017-03-26 18:28:09','cs_499_tcms','2017-04-10 22:08:27','cs_499_tcms',2,'U','0000-00-00 00:00:00',NULL),(3,'ztj0002','Zach','Taylor','Johnson','4545 Over There','Guntersville','AL',35976,'7063156775','7063156775','ztj0002@uah.edu',45,'2012-06-18',1,'Store A','Technician',1,'/yiRAdYrAb0UcwHmvzhmBoRdiEJunKpVEx7PGRyUlQxVDPyy6AzuAgtBvxQ4ZSJEySalH3A0gEVbrPei2qBq2A==','/Zy5BD1RyKI7IGCrD0AE0G/5njrpbAeaL+AvVGIXFYL/hRF6pcdwAW28n7sbCZGyBATP3q/fVv55r5ZxnkYwaKGq2Sapn56ZIzjxjU9EoR9kACG73l/HOJfv4XxfC766OTlLwQXmfJHDk38fCDFtVe0MBOAseOuzr/I6Ekbmu7sDtEwlK2nEQXqJvLLDPsSupcV55VQcw/pYdvkNrrqIP6K4qk8t/a54vspI6pb/gGsmYbypoGYL+ibSrtpC1DLmLvNvPm0IcWWvRKxigZKk3E7nTGiISBvj6fFIvEmpl14QOT9jwkN2PyAECFNl7YFY/zTXRO73kqtDfODbRvXGjg==','2017-03-26 18:28:09','cs_499_tcms','2017-04-10 22:08:49','cs_499_tcms',3,'U','0000-00-00 00:00:00',NULL),(3,'ztj0002','Zach','Taylor','Johnson','4545 Over There','Guntersville','AL',35976,'7063156775','7063156775','ztj0002@uah.edu',45,'2012-06-18',0,'Store A','Technician',1,'/yiRAdYrAb0UcwHmvzhmBoRdiEJunKpVEx7PGRyUlQxVDPyy6AzuAgtBvxQ4ZSJEySalH3A0gEVbrPei2qBq2A==','/Zy5BD1RyKI7IGCrD0AE0G/5njrpbAeaL+AvVGIXFYL/hRF6pcdwAW28n7sbCZGyBATP3q/fVv55r5ZxnkYwaKGq2Sapn56ZIzjxjU9EoR9kACG73l/HOJfv4XxfC766OTlLwQXmfJHDk38fCDFtVe0MBOAseOuzr/I6Ekbmu7sDtEwlK2nEQXqJvLLDPsSupcV55VQcw/pYdvkNrrqIP6K4qk8t/a54vspI6pb/gGsmYbypoGYL+ibSrtpC1DLmLvNvPm0IcWWvRKxigZKk3E7nTGiISBvj6fFIvEmpl14QOT9jwkN2PyAECFNl7YFY/zTXRO73kqtDfODbRvXGjg==','2017-03-26 18:28:09','cs_499_tcms','2017-04-10 22:09:48','dc0059',4,'U','0000-00-00 00:00:00',NULL),(108,'jm0062','Jonathan','something','Mullen','1414 Over There','Huntsville','AL',35801,'5654587878','2564567894','jm0062@uah.edu',65,'2017-04-10',0,'In the corner over there','The other dude',1,'8z2VYkuF+D+vIbWku9sqEaFNLUEr8xIuiam4CLJNt8neN/UXNL+35wXSaxSDIfzYwoC2g8at5Iar0VsiL8L0jw==','zRgGIpGwVMkVv+S+L5Y/AqfiXq/G5zsbLzTgHQpLEgtN14oGP2UiAe6sJkIXVzdsRA81XyHu0c9uevzIzx+lQ/HPXwSIz6gK87JnB81wViI+qjNNJh0reNkzhDfKWMXZrGfNtVZ6o6Gj0/ohs7iv04hQRVLUCSetY3mP/egu/y9AMw6cyRex1DXb5NZ/2Q4j0vn7v94vAhyLjI+n88sJsJZRw3qIJzBYOAzAsOMXONkdMEn3xavcFRusguzOyCT9IoDZzK2dawLsa+pwDudTNv/e+ZFPCkbBvM+9QKwuQrx6w3mirWaanieyXnDaG+q+MKcJuv7/FDBlT32moDrtqw==','2017-04-11 04:18:25','shipper','0000-00-00 00:00:00','shipper',1,'U','0000-00-00 00:00:00',NULL);
/*!40000 ALTER TABLE `user_log` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-14  2:15:28
