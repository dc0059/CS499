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
-- Table structure for table `purchaseitems`
--

DROP TABLE IF EXISTS `purchaseitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchaseitems` (
  `ItemID` int(11) NOT NULL AUTO_INCREMENT,
  `OrderID` int(11) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `PartID` int(11) DEFAULT NULL,
  `PartStatus` varchar(50) DEFAULT 'Shipped',
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL,
  `Version` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`ItemID`),
  UNIQUE KEY `purchaseitems_orderid+partid` (`OrderID`,`PartID`),
  KEY `part` (`PartID`),
  CONSTRAINT `PruchaseOrder` FOREIGN KEY (`OrderID`) REFERENCES `purchaseorder` (`OrderID`),
  CONSTRAINT `part` FOREIGN KEY (`PartID`) REFERENCES `parts` (`PartID`)
) ENGINE=InnoDB AUTO_INCREMENT=108 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `purchaseitems`
--

LOCK TABLES `purchaseitems` WRITE;
/*!40000 ALTER TABLE `purchaseitems` DISABLE KEYS */;
INSERT INTO `purchaseitems` VALUES (3,4,3,2,'Shipped','2017-03-27 20:18:23','johnsza','0000-00-00 00:00:00','johnsza',1),(5,5,14,2,'Shipped','2017-03-29 23:00:37','dc0059','0000-00-00 00:00:00','dc0059',1),(6,4,20,4,'Shipped','2017-04-04 20:45:09','driver','0000-00-00 00:00:00','driver',1),(7,6,10,2,'On Back Order','2017-04-04 22:23:12','driver','0000-00-00 00:00:00','driver',1),(58,26,37,45,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(59,7,2,52,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(60,33,20,38,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(61,37,43,49,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(62,27,9,28,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(63,16,9,46,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(64,44,47,23,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(65,13,20,36,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(66,43,11,28,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(67,21,47,37,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(68,15,44,34,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(69,49,30,37,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(70,12,43,26,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(71,38,47,20,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(72,30,43,40,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(73,54,35,45,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(74,6,4,42,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(75,4,13,24,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(76,4,23,13,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(77,35,50,49,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(78,8,1,44,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(79,25,27,13,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(80,54,42,27,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(81,42,7,30,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(82,53,48,27,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(83,44,2,18,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(84,9,13,19,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(85,9,4,25,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(86,18,26,7,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(87,14,24,12,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(88,37,40,39,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(89,43,36,46,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(90,40,25,14,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(91,21,28,29,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(92,52,48,10,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(93,11,48,30,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(94,46,50,25,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(95,4,34,20,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(96,7,25,11,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(97,42,25,21,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(98,37,43,18,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(99,7,9,16,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(100,35,34,37,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(101,8,2,22,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(102,19,43,49,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(103,33,42,11,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(104,42,28,24,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(105,24,39,8,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(106,12,40,27,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1),(107,22,1,39,'Shipped','2017-04-10 20:05:56','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `purchaseitems` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`purchaseitems_bi` BEFORE INSERT ON cs_499_tcms.purchaseitems FOR EACH ROW 
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`purchaseitems_bu` BEFORE UPDATE ON cs_499_tcms.purchaseitems FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO purchaseitems_log 
( 
ItemID,
OrderID,
Quantity,
PartID,
PartStatus,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.ItemID,
OLD.OrderID,
OLD.Quantity,
OLD.PartID,
OLD.PartStatus,
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`purchaseitems_bd` BEFORE DELETE ON cs_499_tcms.purchaseitems FOR EACH ROW 
BEGIN 
INSERT INTO purchaseitems_log 
( 
ItemID,
OrderID,
Quantity,
PartID,
PartStatus,
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
OLD.ItemID,
OLD.OrderID,
OLD.Quantity,
OLD.PartID,
OLD.PartStatus,
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
