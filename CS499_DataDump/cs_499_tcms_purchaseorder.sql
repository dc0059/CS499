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
-- Table structure for table `purchaseorder`
--

DROP TABLE IF EXISTS `purchaseorder`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchaseorder` (
  `OrderID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderNumber` int(11) DEFAULT NULL,
  `SourceID` int(11) DEFAULT NULL,
  `DestinationID` int(11) DEFAULT NULL,
  `ManifestID` int(11) DEFAULT NULL,
  `PaymentMade` tinyint(4) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL,
  `Version` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`OrderID`),
  UNIQUE KEY `purchaseorder_ordernumber` (`OrderNumber`),
  KEY `Destination` (`DestinationID`),
  KEY `Manifest` (`ManifestID`),
  KEY `Source` (`SourceID`),
  CONSTRAINT `Destination` FOREIGN KEY (`DestinationID`) REFERENCES `businesspartners` (`CompanyID`),
  CONSTRAINT `Manifest` FOREIGN KEY (`ManifestID`) REFERENCES `manifest` (`ManifestID`),
  CONSTRAINT `Source` FOREIGN KEY (`SourceID`) REFERENCES `businesspartners` (`CompanyID`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchaseorder`
--

LOCK TABLES `purchaseorder` WRITE;
/*!40000 ALTER TABLE `purchaseorder` DISABLE KEYS */;
INSERT INTO `purchaseorder` VALUES (4,123,6,5,4,1,'2017-03-26 22:10:01','johnsza','2017-04-06 02:10:58','dc0059',2),(5,12345678,3,2,5,0,'2017-03-29 23:00:03','dc0059','0000-00-00 00:00:00','dc0059',1),(6,122368697,6,4,4,1,'2017-04-04 22:22:33','driver','2017-04-06 02:11:21','dc0059',2),(7,27890,14,33,56,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(8,73513,23,6,38,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(9,68429,17,14,45,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(10,6781,5,40,53,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(11,50586,36,25,40,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(12,83650,32,15,5,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(13,78993,28,18,58,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(14,68156,31,35,35,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(15,45037,41,40,45,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(16,51466,24,28,57,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(17,80425,34,19,51,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(18,3765,29,4,62,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(19,50573,40,39,55,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(20,20658,42,22,4,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(21,14847,39,20,61,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(22,99200,35,40,41,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(23,40624,20,6,35,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(24,68901,21,31,43,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(25,459,18,25,55,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(26,49154,22,42,58,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(27,22909,17,21,41,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(28,42299,14,24,46,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(29,94351,17,27,59,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(30,57027,19,30,5,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(31,92368,18,19,42,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(32,13359,33,34,57,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(33,5751,29,32,44,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(34,21125,38,6,4,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(35,17998,37,15,40,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(36,21067,20,25,61,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(37,56442,21,42,56,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(38,47161,15,6,52,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(39,31560,33,31,37,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(40,52495,25,39,5,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(41,65759,23,20,60,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(42,60643,17,23,62,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(43,66523,38,6,40,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(44,43504,41,5,50,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(45,47580,14,33,40,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(46,8672,42,30,43,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(47,25288,22,26,45,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(48,56625,18,39,46,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(49,13615,35,15,54,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(50,77096,19,18,36,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(51,64439,27,42,46,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(52,15776,15,30,41,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(53,61202,42,6,45,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(54,78221,30,27,58,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(55,22314,36,15,35,1,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(56,18100,6,42,57,0,'2017-04-10 19:31:23','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `purchaseorder` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`purchaseorder_bi` BEFORE INSERT ON cs_499_tcms.purchaseorder FOR EACH ROW 
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`purchaseorder_bu` BEFORE UPDATE ON cs_499_tcms.purchaseorder FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO purchaseorder_log 
( 
OrderID,
OrderNumber,
SourceID,
DestinationID,
ManifestID,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.OrderID,
OLD.OrderNumber,
OLD.SourceID,
OLD.DestinationID,
OLD.ManifestID,
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`purchaseorder_bd` BEFORE DELETE ON cs_499_tcms.purchaseorder FOR EACH ROW 
BEGIN 
INSERT INTO purchaseorder_log 
( 
OrderID,
OrderNumber,
SourceID,
DestinationID,
ManifestID,
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
OLD.OrderID,
OLD.OrderNumber,
OLD.SourceID,
OLD.DestinationID,
OLD.ManifestID,
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
