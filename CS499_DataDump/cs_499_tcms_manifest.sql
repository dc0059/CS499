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
-- Table structure for table `manifest`
--

DROP TABLE IF EXISTS `manifest`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `manifest` (
  `ManifestID` int(11) NOT NULL AUTO_INCREMENT,
  `ShipmentType` varchar(11) DEFAULT NULL,
  `VehicleID` int(11) DEFAULT NULL,
  `DepartureTime` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `ETA` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `Arrived` tinyint(20) DEFAULT NULL,
  `ShippingCost` double DEFAULT NULL,
  `EmployeeID` int(11) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL,
  `Version` int(11) DEFAULT '1',
  PRIMARY KEY (`ManifestID`),
  KEY `Vehicle` (`VehicleID`),
  KEY `employee` (`EmployeeID`),
  CONSTRAINT `Vehicle` FOREIGN KEY (`VehicleID`) REFERENCES `vehicle` (`VehicleID`),
  CONSTRAINT `employee` FOREIGN KEY (`EmployeeID`) REFERENCES `user` (`EmployeeID`)
) ENGINE=InnoDB AUTO_INCREMENT=64 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `manifest`
--

LOCK TABLES `manifest` WRITE;
/*!40000 ALTER TABLE `manifest` DISABLE KEYS */;
INSERT INTO `manifest` VALUES (4,'Outgoing',4,'2012-10-02 05:00:00','2012-10-04 05:00:00',0,20000,4,'2017-03-26 21:48:55','johnsza','2017-04-04 19:14:16','dc0059',4),(5,'Outgoing',5,'2017-03-29 22:58:35','2017-04-05 22:58:35',0,1400,1,'2017-03-29 22:59:03','dc0059','0000-00-00 00:00:00','dc0059',1),(35,'Incoming',16,'2011-10-02 00:37:55','2013-01-17 10:21:02',0,30208.33,4,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(36,'Outgoing',27,'2007-07-28 15:46:48','2015-11-21 19:01:03',1,19679.94,26,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(37,'Outgoing',4,'2007-12-25 12:01:33','2015-08-27 14:38:31',1,20013.32,61,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(38,'Outgoing',12,'2008-11-20 00:40:15','2017-01-17 11:51:33',0,17052.66,33,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(39,'Outgoing',30,'2007-07-04 20:25:00','2016-02-15 22:27:21',0,39633.98,10,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(40,'Incoming',5,'2011-11-29 20:58:53','2015-06-10 09:12:51',0,19956.09,11,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(41,'Outgoing',25,'2010-09-14 04:08:52','2014-12-31 13:15:02',0,33567.68,40,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(42,'Outgoing',15,'2007-05-22 08:33:53','2014-09-21 03:54:21',0,42667.71,87,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(43,'Outgoing',8,'2010-02-10 08:02:40','2016-11-02 19:16:02',1,40077.74,90,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(44,'Incoming',19,'2008-05-22 19:39:52','2013-08-01 21:57:57',1,22401.56,104,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(45,'Incoming',20,'2008-06-29 11:10:54','2012-11-16 22:01:26',0,35022.92,66,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(46,'Outgoing',23,'2008-07-18 11:29:39','2012-08-09 02:08:54',0,49425.99,70,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(47,'Incoming',17,'2012-03-14 19:38:40','2015-05-25 21:03:41',1,21045.19,26,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(48,'Incoming',13,'2008-04-26 23:25:07','2017-01-26 21:21:40',0,41388.67,26,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(49,'Incoming',12,'2010-06-01 21:17:00','2013-04-08 17:37:29',0,10575.56,90,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(50,'Outgoing',6,'2012-01-15 06:56:44','2015-04-26 23:40:11',0,20762.73,61,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(51,'Incoming',10,'2011-12-08 04:48:53','2013-05-10 20:03:03',1,40544.72,107,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(52,'Incoming',11,'2007-05-24 04:27:15','2016-08-06 17:33:25',0,45762.25,84,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(53,'Incoming',29,'2010-01-22 02:51:32','2015-09-27 22:53:10',1,49003.25,77,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(54,'Incoming',28,'2008-01-26 16:18:05','2015-09-22 18:07:05',1,12491.97,8,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(55,'Incoming',18,'2007-07-04 09:58:48','2016-07-02 11:36:54',1,38923.59,62,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(56,'Outgoing',16,'2008-07-07 15:21:04','2016-04-24 12:47:37',0,38299.75,22,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(57,'Incoming',7,'2007-10-04 20:10:15','2015-12-01 05:49:02',0,30406.03,23,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(58,'Outgoing',21,'2008-01-19 04:24:24','2016-12-11 02:04:18',1,26270.11,75,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(59,'Incoming',22,'2011-11-16 13:35:55','2012-07-11 17:54:04',1,16796.99,9,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(60,'Outgoing',24,'2011-09-30 11:06:03','2014-03-12 19:07:28',1,46493.33,20,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(61,'Incoming',9,'2010-11-05 07:14:09','2012-09-11 19:12:18',0,26909.82,104,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(62,'Outgoing',14,'2007-04-30 20:47:52','2013-10-18 15:09:02',1,10349.41,4,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1),(63,'Outgoing',18,'2012-03-21 03:57:48','2016-02-07 03:39:13',1,43362.94,11,'2017-04-10 15:37:16','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `manifest` ENABLE KEYS */;
UNLOCK TABLES;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`manifest_bi` BEFORE INSERT ON cs_499_tcms.manifest FOR EACH ROW 
BEGIN 
 
SET NEW.CREATEDDATE = CURRENT_TIMESTAMP; 
SET NEW.VERSION = 1; 
 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`manifest_bu` BEFORE UPDATE ON cs_499_tcms.manifest FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO manifest_log 
( 
ManifestID,
ShipmentType,
VehicleID,
DepartureTime,
ETA,
Arrived,
ShippingCost,
EmployeeID,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.ManifestID,
OLD.ShipmentType,
OLD.VehicleID,
OLD.DepartureTime,
OLD.ETA,
OLD.Arrived,
OLD.ShippingCost,
OLD.EmployeeID,
OLD.CreatedDate,
OLD.CreatedBy,
OLD.LastModifiedDate,
OLD.LastModifiedBy,
OLD.Version,
'U'
); 
 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`manifest_bd` BEFORE DELETE ON cs_499_tcms.manifest FOR EACH ROW 
BEGIN 
INSERT INTO manifest_log 
( 
ManifestID,
ShipmentType,
VehicleID,
DepartureTime,
ETA,
Arrived,
ShippingCost,
EmployeeID,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus,
DeletedDate 
) 
VALUES 
( 
OLD.ManifestID,
OLD.ShipmentType,
OLD.VehicleID,
OLD.DepartureTime,
OLD.ETA,
OLD.Arrived,
OLD.ShippingCost,
OLD.EmployeeID,
OLD.CreatedDate,
OLD.CreatedBy,
OLD.LastModifiedDate,
OLD.LastModifiedBy,
OLD.Version,
'D',
CURRENT_TIMESTAMP 
); 
 
END */;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-14  2:15:28
