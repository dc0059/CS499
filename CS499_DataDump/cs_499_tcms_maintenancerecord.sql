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
-- Table structure for table `maintenancerecord`
--

DROP TABLE IF EXISTS `maintenancerecord`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `maintenancerecord` (
  `MaintenanceID` int(11) NOT NULL AUTO_INCREMENT,
  `VehicleID` int(11) DEFAULT NULL,
  `MaintenanceDate` date DEFAULT NULL,
  `MaintenanceDescription` varchar(4000) CHARACTER SET latin1 DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL,
  `Version` int(11) DEFAULT '1',
  PRIMARY KEY (`MaintenanceID`),
  KEY `Truck_ID` (`VehicleID`),
  CONSTRAINT `Truck_ID` FOREIGN KEY (`VehicleID`) REFERENCES `vehicle` (`VehicleID`)
) ENGINE=InnoDB AUTO_INCREMENT=52 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `maintenancerecord`
--

LOCK TABLES `maintenancerecord` WRITE;
/*!40000 ALTER TABLE `maintenancerecord` DISABLE KEYS */;
INSERT INTO `maintenancerecord` VALUES (2,4,'2008-04-12','Stuff happened.','2017-03-27 16:58:50','johnsza','0000-00-00 00:00:00','johnsza',1),(3,15,'2012-03-08','eget lacus. Mauris non dui nec urna suscipit nonummy. Fusce','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(4,19,'2014-09-12','mus. Donec dignissim magna a tortor. Nunc commodo auctor velit.','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(5,27,'2014-03-12','id sapien. Cras dolor dolor, tempus non, lacinia at, iaculis','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(6,18,'2012-11-12','arcu. Sed eu nibh vulputate mauris sagittis placerat. Cras dictum','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(7,8,'2010-01-03','Suspendisse dui. Fusce diam nunc, ullamcorper eu, euismod ac, fermentum','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(8,29,'2011-01-13','a tortor. Nunc commodo auctor velit. Aliquam nisl. Nulla eu','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(9,28,'2007-10-13','consectetuer mauris id sapien. Cras dolor dolor, tempus non, lacinia','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(10,23,'2009-04-09','non, lacinia at, iaculis quis, pede. Praesent eu dui. Cum','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(11,8,'2015-07-04','orci luctus et ultrices posuere cubilia Curae; Donec tincidunt. Donec','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(12,12,'2012-12-25','Pellentesque ut ipsum ac mi eleifend egestas. Sed pharetra, felis','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(13,15,'2013-11-04','erat. Sed nunc est, mollis non, cursus non, egestas a,','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(14,10,'2012-12-19','volutpat. Nulla facilisis. Suspendisse commodo tincidunt nibh. Phasellus nulla. Integer','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(15,29,'2013-10-11','lectus convallis est, vitae sodales nisi magna sed dui. Fusce','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(16,29,'2009-10-29','purus. Nullam scelerisque neque sed sem egestas blandit. Nam nulla','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(17,5,'2012-05-17','purus. Duis elementum, dui quis accumsan convallis, ante lectus convallis','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(18,7,'2013-10-21','molestie dapibus ligula. Aliquam erat volutpat. Nulla dignissim. Maecenas ornare','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(19,15,'2010-12-03','consectetuer adipiscing elit. Curabitur sed tortor. Integer aliquam adipiscing lacus.','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(20,11,'2016-04-30','quam a felis ullamcorper viverra. Maecenas iaculis aliquet diam. Sed','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(21,19,'2015-08-18','aliquam adipiscing lacus. Ut nec urna et arcu imperdiet ullamcorper.','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(22,29,'2016-04-28','vitae dolor. Donec fringilla. Donec feugiat metus sit amet ante.','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(23,18,'2014-08-15','blandit. Nam nulla magna, malesuada vel, convallis in, cursus et,','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(24,27,'2015-12-27','accumsan neque et nunc. Quisque ornare tortor at risus. Nunc','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(25,29,'2015-05-30','magna. Suspendisse tristique neque venenatis lacus. Etiam bibendum fermentum metus.','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(26,15,'2009-05-17','porttitor eros nec tellus. Nunc lectus pede, ultrices a, auctor','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(27,25,'2016-09-06','vulputate velit eu sem. Pellentesque ut ipsum ac mi eleifend','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(28,10,'2008-02-25','tempus eu, ligula. Aenean euismod mauris eu elit. Nulla facilisi.','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(29,17,'2016-11-03','posuere at, velit. Cras lorem lorem, luctus ut, pellentesque eget,','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(30,6,'2009-01-13','hendrerit. Donec porttitor tellus non magna. Nam ligula elit, pretium','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(31,4,'2014-11-03','facilisis facilisis, magna tellus faucibus leo, in lobortis tellus justo','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(32,12,'2009-09-12','nibh lacinia orci, consectetuer euismod est arcu ac orci. Ut','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(33,14,'2011-08-22','tortor, dictum eu, placerat eget, venenatis a, magna. Lorem ipsum','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(34,23,'2007-07-31','mi fringilla mi lacinia mattis. Integer eu lacus. Quisque imperdiet,','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(35,15,'2008-09-04','pretium neque. Morbi quis urna. Nunc quis arcu vel quam','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(36,20,'2015-03-07','pede. Praesent eu dui. Cum sociis natoque penatibus et magnis','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(37,8,'2016-07-06','aliquam eros turpis non enim. Mauris quis turpis vitae purus','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(38,11,'2008-04-01','In at pede. Cras vulputate velit eu sem. Pellentesque ut','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(39,18,'2016-02-27','euismod mauris eu elit. Nulla facilisi. Sed neque. Sed eget','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(40,21,'2015-10-19','magnis dis parturient montes, nascetur ridiculus mus. Proin vel nisl.','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(41,27,'2016-10-13','facilisis vitae, orci. Phasellus dapibus quam quis diam. Pellentesque habitant','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(42,9,'2015-04-12','scelerisque dui. Suspendisse ac metus vitae velit egestas lacinia. Sed','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(43,16,'2013-12-21','tristique pharetra. Quisque ac libero nec ligula consectetuer rhoncus. Nullam','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(44,25,'2012-06-05','sit amet, consectetuer adipiscing elit. Aliquam auctor, velit eget laoreet','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(45,12,'2012-02-22','massa. Mauris vestibulum, neque sed dictum eleifend, nunc risus varius','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(46,5,'2013-11-28','vel est tempor bibendum. Donec felis orci, adipiscing non, luctus','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(47,7,'2011-05-16','felis purus ac tellus. Suspendisse sed dolor. Fusce mi lorem,','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(48,8,'2016-08-22','dis parturient montes, nascetur ridiculus mus. Donec dignissim magna a','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(49,6,'2016-12-30','cursus vestibulum. Mauris magna. Duis dignissim tempor arcu. Vestibulum ut','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(50,22,'2008-08-28','aliquet magna a neque. Nullam ut nisi a odio semper','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1),(51,22,'2017-02-13','non enim. Mauris quis turpis vitae purus gravida sagittis. Duis','2017-04-12 14:48:57','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `maintenancerecord` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancerecord_bi` BEFORE INSERT ON cs_499_tcms.maintenancerecord FOR EACH ROW 
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancerecord_bu` BEFORE UPDATE ON cs_499_tcms.maintenancerecord FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO maintenancerecord_log 
( 
MaintenanceID,
VehicleID,
MaintenanceDate,
MaintenanceDescription,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.MaintenanceID,
OLD.VehicleID,
OLD.MaintenanceDate,
OLD.MaintenanceDescription,
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`maintenancerecord_bd` BEFORE DELETE ON cs_499_tcms.maintenancerecord FOR EACH ROW 
BEGIN 
INSERT INTO maintenancerecord_log 
( 
MaintenanceID,
VehicleID,
MaintenanceDate,
MaintenanceDescription,
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
OLD.MaintenanceID,
OLD.VehicleID,
OLD.MaintenanceDate,
OLD.MaintenanceDescription,
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

-- Dump completed on 2017-04-14  2:15:29
