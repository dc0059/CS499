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
-- Table structure for table `maintenancepart`
--

DROP TABLE IF EXISTS `maintenancepart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `maintenancepart` (
  `MaintenancePartID` int(11) NOT NULL AUTO_INCREMENT,
  `Quantity` int(11) DEFAULT NULL,
  `MaintenanceRecordID` int(11) DEFAULT NULL,
  `PartID` int(11) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL,
  `Version` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`MaintenancePartID`),
  UNIQUE KEY `maintenancepart_MaintenanceID+PartID` (`MaintenanceRecordID`,`PartID`),
  KEY `partid` (`PartID`),
  CONSTRAINT `Maintenance_ID` FOREIGN KEY (`MaintenanceRecordID`) REFERENCES `maintenancerecorddetails` (`DetailID`),
  CONSTRAINT `partid` FOREIGN KEY (`PartID`) REFERENCES `parts` (`PartID`)
) ENGINE=InnoDB AUTO_INCREMENT=105 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maintenancepart`
--

LOCK TABLES `maintenancepart` WRITE;
/*!40000 ALTER TABLE `maintenancepart` DISABLE KEYS */;
INSERT INTO `maintenancepart` VALUES (3,2,2,2,'2017-03-30 22:28:08','dc0059','2017-04-09 22:00:09','dc0059',2),(4,2,3,4,'2017-04-09 21:59:39','dc0059','2017-04-09 22:00:34','dc0059',2),(55,28,19,22,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(56,34,22,46,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(57,24,13,12,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(58,24,51,28,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(59,7,45,42,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(60,29,31,18,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(61,5,39,6,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(62,37,15,44,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(63,33,52,6,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(64,24,14,30,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(65,8,20,46,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(66,33,38,47,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(67,37,48,31,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(68,39,46,37,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(69,43,3,43,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(70,9,50,12,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(71,31,6,4,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(72,27,33,37,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(73,28,21,12,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(74,12,48,35,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(75,6,42,51,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(76,1,35,11,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(77,11,32,10,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(78,39,16,35,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(79,26,39,22,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(80,16,8,36,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(81,50,14,10,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(82,14,50,51,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(83,38,4,43,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(84,14,4,10,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(85,10,44,41,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(86,45,25,28,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(87,9,49,12,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(88,10,23,4,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(89,30,37,4,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(90,14,12,10,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(91,10,38,28,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(92,16,36,20,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(93,18,44,42,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(94,39,37,50,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(95,36,36,38,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(96,28,24,28,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(97,46,17,47,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(98,44,19,29,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(99,47,3,19,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(100,29,37,32,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(101,20,17,34,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(102,12,9,27,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(103,33,17,27,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1),(104,47,48,17,'2017-04-12 15:13:03','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `maintenancepart` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancepart_bi` BEFORE INSERT ON cs_499_tcms.maintenancepart FOR EACH ROW 
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancepart_bu` BEFORE UPDATE ON cs_499_tcms.maintenancepart FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO maintenancepart_log 
( 
MaintenancePartID,
Quantity,
MaintenanceRecordID,
PartID,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.MaintenancePartID,
OLD.Quantity,
OLD.MaintenanceRecordID,
OLD.PartID,
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancepart_bd` BEFORE DELETE ON cs_499_tcms.maintenancepart FOR EACH ROW 
BEGIN 
INSERT INTO maintenancepart_log 
( 
MaintenancePartID,
Quantity,
MaintenanceRecordID,
PartID,
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
OLD.MaintenancePartID,
OLD.Quantity,
OLD.MaintenanceRecordID,
OLD.PartID,
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
