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
-- Table structure for table `parts`
--

DROP TABLE IF EXISTS `parts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `parts` (
  `PartID` int(11) NOT NULL AUTO_INCREMENT,
  `PartDescription` varchar(100) CHARACTER SET latin1 DEFAULT NULL,
  `PartNumber` int(11) DEFAULT NULL,
  `PartPrice` double DEFAULT NULL,
  `PartWeight` int(11) DEFAULT NULL,
  `QuantityInStock` int(11) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL DEFAULT 'Default',
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL DEFAULT 'Default',
  `Version` int(11) DEFAULT '1',
  PRIMARY KEY (`PartID`),
  UNIQUE KEY `parts_partnumber` (`PartNumber`)
) ENGINE=InnoDB AUTO_INCREMENT=84 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `parts`
--

LOCK TABLES `parts` WRITE;
/*!40000 ALTER TABLE `parts` DISABLE KEYS */;
INSERT INTO `parts` VALUES (2,'A part.',13245,25,10,200,'2017-03-26 18:28:09','johnsza','0000-00-00 00:00:00','johnsza',1),(4,'Tire',123456789,1000,150,3000,'2017-03-29 23:02:24','dc0059','0000-00-00 00:00:00','dc0059',1),(5,'Spark plug',33611,318,67,712,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(6,'Battery',32155,666,87,791,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(7,'Glowplug',7199,213,90,155,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(8,'Nut',130,897,71,222,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(9,'Headlight bulb',44839,260,82,857,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(10,'Brake fluid',63711,889,86,589,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(11,'Mudflap',59441,781,70,657,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(12,'Bolt',94344,991,53,646,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(13,'Engine oil',30664,294,82,817,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(14,'Screw',93923,384,90,954,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(15,'Spring',38977,518,89,983,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(16,'Widget',14445,615,61,546,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(17,'Doohickey',46656,926,66,135,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(18,'Gizmo',18102,347,82,331,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(19,'Gadget',51917,720,71,741,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(20,'Somali Pirate',2459,463,98,429,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(21,'Cable spool',38099,347,75,472,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(22,'Wooden pallet',25040,467,98,370,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(23,'Winch',52029,69,55,201,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(24,'Ratchet',17393,178,68,653,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(25,'Front bumper',23226,814,82,964,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(26,'Wiper fluid',30127,987,89,453,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(27,'Rear bumper',94397,670,57,506,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(28,'Rear-view mirror',23943,891,77,766,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(29,'Sleeper',83043,737,69,108,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(30,'Side-view mirror',29534,368,61,452,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(31,'Radiator',21835,804,51,480,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(32,'Axle',97735,158,87,126,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(33,'Wheel',71023,533,83,854,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(34,'Transmission',4984,357,55,624,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(35,'Fuel tank',58639,549,57,163,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(36,'Transmission fluid',80010,152,100,833,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(37,'Grill',79574,388,87,832,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(38,'Shock absorber',40257,728,61,181,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(39,'Fuel filter',43995,733,85,970,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(40,'Air filter',89987,348,77,568,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(41,'Oil filter',10065,257,72,407,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(42,'Air bag assembly',50631,22,50,420,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(43,'Door',13361,296,59,440,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(44,'Window',2155,958,98,823,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(45,'Windshield',35988,742,68,631,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(46,'Headlamp',83265,365,98,404,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(47,'Axle shaft',25364,212,81,976,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(48,'Fender extension',76227,468,89,749,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(49,'Steering gear',12141,678,58,992,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(50,'Tank fairing',37606,293,95,803,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(51,'Air cooler',98419,255,70,769,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(52,'Coolant',1951,945,74,731,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(53,'Hood',75825,770,71,633,'2017-04-09 23:56:46','dc0059','0000-00-00 00:00:00','dc0059',1),(54,'Laptop computer',1,239,15,811,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(55,'3D printer',2,269.99,50,273,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(56,'Flatscreen television',3,697.99,31,767,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(57,'Tablet',4,139,3,514,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(58,'Clothing',5,180,27,425,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(59,'Bottled water',6,120,43,176,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(60,'Nintendo Switch',7,489.94,16,726,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(61,'Washer',8,299,97,320,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(62,'Lounge chair',9,249.99,45,915,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(63,'Bed frame (queen-size)',10,351.49,90,663,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(64,'Lumber',11,660.66,78,582,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(65,'Electrical wire',12,156,45,950,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(66,'PVC pipes',13,729.19,77,591,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(67,'Black iron pipes',14,1172.5,70,531,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(68,'Bed frame (king-size)',15,383.99,110,606,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(69,'Mattress (queen-size)',16,689.99,99,829,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(70,'Dryer',17,399,98,170,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(71,'Insulation',18,59.98,22,994,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(72,'Lawn mower (electric)',19,164.99,79,657,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(73,'Foldable bike',20,120.99,10,417,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(74,'Somalian pirate hunters',21,355.69,48,380,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(75,'Deluxe premium frisbee',22,206,2,923,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(76,'Portable generator',23,1045,52,873,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(77,'Camping set',24,162,40,418,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(78,'Bag of sand',25,767,61,543,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(79,'Glass pane',26,17.98,23,238,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(80,'Inkjet printer',27,59.97,18,620,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(81,'Samophlange assembly',28,567,52,151,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(82,'Hunter\'s starter kit',29,257,20,438,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1),(83,'Lead bricks',30,240,89,528,'2017-04-10 22:26:23','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `parts` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`parts_bi` BEFORE INSERT ON cs_499_tcms.parts FOR EACH ROW 
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`parts_bu` BEFORE UPDATE ON cs_499_tcms.parts FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO parts_log 
( 
PartID,
PartDescription,
PartNumber,
PartPrice,
PartWeight,
QuantityInStock,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.PartID,
OLD.PartDescription,
OLD.PartNumber,
OLD.PartPrice,
OLD.PartWeight,
OLD.QuantityInStock,
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`parts_bd` BEFORE DELETE ON cs_499_tcms.parts FOR EACH ROW 
BEGIN 
INSERT INTO parts_log 
( 
PartID,
PartDescription,
PartNumber,
PartPrice,
PartWeight,
QuantityInStock,
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
OLD.PartID,
OLD.PartDescription,
OLD.PartNumber,
OLD.PartPrice,
OLD.PartWeight,
OLD.QuantityInStock,
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
