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
-- Table structure for table `maintenancerecorddetails`
--

DROP TABLE IF EXISTS `maintenancerecorddetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `maintenancerecorddetails` (
  `DetailID` int(11) NOT NULL AUTO_INCREMENT,
  `MaintenanceID` int(11) DEFAULT NULL,
  `EmployeeID` int(11) DEFAULT NULL,
  `RepairDescription` varchar(4000) CHARACTER SET latin1 DEFAULT NULL,
  `RepairDate` date DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL,
  `Version` int(11) DEFAULT '1',
  PRIMARY KEY (`DetailID`),
  KEY `EmployeeID` (`EmployeeID`),
  KEY `Maintenance_Record_ID` (`MaintenanceID`),
  CONSTRAINT `EmployeeID` FOREIGN KEY (`EmployeeID`) REFERENCES `user` (`EmployeeID`),
  CONSTRAINT `Maintenance_Record_ID` FOREIGN KEY (`MaintenanceID`) REFERENCES `maintenancerecord` (`MaintenanceID`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maintenancerecorddetails`
--

LOCK TABLES `maintenancerecorddetails` WRITE;
/*!40000 ALTER TABLE `maintenancerecorddetails` DISABLE KEYS */;
INSERT INTO `maintenancerecorddetails` VALUES (2,2,1,'Stuff happened.','2010-08-14','2017-03-27 17:23:22','johnsza','0000-00-00 00:00:00','johnsza',1),(3,2,4,'Changed tire.','2017-04-09','2017-04-09 21:59:07','dc0059','0000-00-00 00:00:00','dc0059',1),(4,21,25,'Nunc mauris. Morbi non','2010-07-11','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(5,23,37,'a nunc. In at','2016-12-02','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(6,23,97,'egestas hendrerit neque. In','2011-06-22','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(7,38,51,'imperdiet non, vestibulum nec,','2007-05-13','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(8,14,36,'volutpat. Nulla dignissim. Maecenas','2009-11-22','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(9,37,47,'ut aliquam iaculis, lacus','2013-01-13','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(10,47,21,'dolor vitae dolor. Donec','2010-04-18','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(11,6,34,'mauris, aliquam eu, accumsan','2009-08-15','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(12,41,29,'Praesent eu nulla at','2012-11-20','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(13,25,25,'Aliquam ornare, libero at','2010-09-09','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(14,49,3,'eu dui. Cum sociis','2010-08-21','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(15,46,12,'Nam consequat dolor vitae','2015-06-12','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(16,25,97,'varius ultrices, mauris ipsum','2016-03-03','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(17,26,73,'Fusce dolor quam, elementum','2012-12-08','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(18,6,60,'facilisis facilisis, magna tellus','2011-02-11','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(19,39,79,'sed dolor. Fusce mi','2013-06-13','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(20,24,16,'nisi. Cum sociis natoque','2011-08-29','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(21,20,31,'Etiam vestibulum massa rutrum','2014-05-09','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(22,27,34,'elit, pretium et, rutrum','2010-02-21','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(23,15,52,'ligula. Aenean gravida nunc','2009-09-03','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(24,5,42,'dolor dapibus gravida. Aliquam','2016-06-13','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(25,26,47,'quam quis diam. Pellentesque','2010-06-11','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(26,9,79,'amet metus. Aliquam erat','2009-11-18','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(27,16,73,'vitae semper egestas, urna','2013-07-28','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(28,10,58,'adipiscing elit. Curabitur sed','2013-03-10','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(29,5,60,'Ut sagittis lobortis mauris.','2013-05-23','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(30,16,3,'magna, malesuada vel, convallis','2015-12-24','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(31,11,12,'Maecenas malesuada fringilla est.','2009-06-07','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(32,33,16,'varius et, euismod et,','2011-04-22','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(33,11,97,'facilisis non, bibendum sed,','2009-03-31','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(34,12,6,'sit amet, dapibus id,','2008-03-20','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(35,23,34,'et magnis dis parturient','2011-07-26','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(36,12,21,'aliquam adipiscing lacus. Ut','2007-12-03','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(37,31,25,'ornare, libero at auctor','2010-08-16','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(38,31,36,'nibh sit amet orci.','2009-07-07','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(39,26,47,'vulputate, posuere vulputate, lacus.','2009-01-25','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(40,16,51,'sagittis placerat. Cras dictum','2009-01-15','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(41,22,34,'metus facilisis lorem tristique','2011-02-05','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(42,5,92,'purus. Nullam scelerisque neque','2012-01-18','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(43,36,97,'nascetur ridiculus mus. Proin','2012-10-14','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(44,18,31,'eget, ipsum. Donec sollicitudin','2016-04-22','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(45,45,16,'eget metus eu erat','2011-12-29','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(46,11,36,'justo sit amet nulla.','2012-03-14','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(47,30,6,'sit amet, dapibus id,','2014-05-04','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(48,9,3,'velit eget laoreet posuere,','2013-12-29','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(49,29,52,'gravida sit amet, dapibus','2007-07-09','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(50,42,92,'lacus pede sagittis augue,','2011-09-22','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(51,6,34,'ipsum. Phasellus vitae mauris','2008-12-22','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(52,45,3,'nec metus facilisis lorem','2008-04-29','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1),(53,41,58,'mollis non, cursus non,','2011-11-23','2017-04-12 15:04:22','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `maintenancerecorddetails` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancerecorddetails_bi` BEFORE INSERT ON cs_499_tcms.maintenancerecorddetails FOR EACH ROW 
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancerecorddetails_bu` BEFORE UPDATE ON cs_499_tcms.maintenancerecorddetails FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO maintenancerecorddetails_log 
( 
DetailID,
MaintenanceID,
EmployeeID,
RepairDescription,
RepairDate,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.DetailID,
OLD.MaintenanceID,
OLD.EmployeeID,
OLD.RepairDescription,
OLD.RepairDate,
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancerecorddetails_bd` BEFORE DELETE ON cs_499_tcms.maintenancerecorddetails FOR EACH ROW 
BEGIN 
INSERT INTO maintenancerecorddetails_log 
( 
DetailID,
MaintenanceID,
EmployeeID,
RepairDescription,
RepairDate,
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
OLD.DetailID,
OLD.MaintenanceID,
OLD.EmployeeID,
OLD.RepairDescription,
OLD.RepairDate,
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
