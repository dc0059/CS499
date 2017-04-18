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
-- Table structure for table `payroll`
--

DROP TABLE IF EXISTS `payroll`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payroll` (
  `PayrollID` int(11) NOT NULL AUTO_INCREMENT,
  `EmployeeID` int(11) DEFAULT NULL,
  `PaymentDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `Payment` double DEFAULT NULL,
  `HoursWorked` double DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL DEFAULT 'Default',
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL DEFAULT 'Default',
  `Version` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`PayrollID`),
  KEY `payroll_ibfk_1` (`EmployeeID`),
  CONSTRAINT `payroll_ibfk_1` FOREIGN KEY (`EmployeeID`) REFERENCES `user` (`EmployeeID`)
) ENGINE=InnoDB AUTO_INCREMENT=106 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payroll`
--

LOCK TABLES `payroll` WRITE;
/*!40000 ALTER TABLE `payroll` DISABLE KEYS */;
INSERT INTO `payroll` VALUES (3,1,'2017-03-26 00:28:31',4000,80,'2017-03-26 00:28:55','dc0059','2017-04-06 01:15:50','dc0059',2),(4,3,'2017-04-01 05:00:00',225,5,'2017-04-05 19:39:01','Default','2017-04-06 01:16:00','dc0059',2),(5,4,'2017-05-21 20:55:00',3000,30,'2017-04-05 19:54:26','Default','2017-04-06 01:30:18','dc0059',4),(6,1,'2008-11-23 01:53:49',7215.25,11.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(7,3,'2013-09-13 01:28:09',6919.63,29.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(8,7,'2015-05-31 17:19:28',1300.35,15.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(9,10,'2015-08-31 04:31:03',6056.72,23.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(10,12,'2009-12-29 12:08:01',9956.96,16.4,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(11,6,'2016-08-19 19:17:35',1892.23,28.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(12,11,'2009-01-20 01:39:06',821.5,17.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(13,16,'2008-06-02 08:54:06',4146.83,18.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(14,18,'2011-10-06 03:25:13',6681.75,24.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(15,14,'2011-08-11 10:18:36',819.04,2.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(16,24,'2014-11-21 17:09:09',938.46,15.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(17,23,'2013-11-16 11:22:07',2315.08,6.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(18,4,'2013-03-26 00:41:19',8112.44,19.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(19,30,'2012-06-12 09:03:44',688.65,31.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(20,29,'2009-07-12 21:43:44',5607.4,9.9,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(21,38,'2013-07-31 16:35:56',8224.73,33.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(22,36,'2010-07-11 02:25:04',64.69,34.6,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(23,34,'2014-06-19 13:00:04',1115.11,12.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(24,45,'2011-05-16 17:41:16',4514.35,4.9,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(25,46,'2015-06-19 12:14:52',9.59,40,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(26,41,'2014-06-21 11:56:49',5473.56,19.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(27,57,'2012-11-13 03:03:36',7355.72,25.4,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(28,52,'2011-07-08 21:43:37',7233.67,26,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(29,68,'2012-10-15 14:30:38',910.31,29.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(30,55,'2015-07-14 16:31:53',8578.88,27.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(31,65,'2013-05-01 18:20:35',5003.63,31.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(32,66,'2011-04-11 20:18:46',316.19,31.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(33,70,'2008-05-20 22:41:22',131.48,39.6,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(34,91,'2008-09-14 09:12:48',6349.31,9.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(35,92,'2013-10-27 14:21:58',7118.87,10.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(36,84,'2017-01-05 15:49:35',7787.84,10,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(37,87,'2011-05-10 03:50:03',8026.3,14.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(38,100,'2016-04-13 00:14:19',4039.42,14.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(39,99,'2008-12-15 22:27:54',9122.65,36.6,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(40,94,'2007-12-12 13:34:10',4786.06,19.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(41,96,'2013-10-27 05:22:13',1897.17,36.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(42,97,'2012-12-17 15:22:08',4280.75,26.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(43,106,'2014-02-03 13:09:09',3047.33,16.6,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(44,105,'2007-10-31 17:01:58',2997.1,36.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(45,104,'2008-02-21 08:31:47',3387.66,39.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(46,13,'2007-10-25 15:12:38',4116.49,11.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(47,15,'2017-01-11 23:43:48',3283.77,12.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(48,8,'2009-03-30 05:21:32',2076.79,15.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(49,17,'2014-08-18 06:20:34',9217.12,33.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(50,9,'2013-07-11 03:29:46',7337.03,3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(51,28,'2007-06-22 02:09:24',6759.2,4.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(52,25,'2015-09-30 09:09:59',9436.29,4,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(53,39,'2014-10-16 10:54:24',8824.96,10,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(54,21,'2016-02-23 15:31:46',5694.95,18.9,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(55,32,'2009-11-08 13:03:34',360.8,33.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(56,42,'2011-06-28 05:49:48',7822.87,37.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(57,54,'2009-10-29 21:17:38',4266.16,11.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(58,50,'2011-03-01 07:23:01',36.53,11.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(59,43,'2014-04-06 12:35:28',8768.52,33.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(60,51,'2014-06-11 07:55:15',6404.48,16.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(61,53,'2011-02-19 08:01:53',8030.18,2.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(62,60,'2009-09-18 02:49:54',8748.19,28,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(63,63,'2014-05-10 22:21:15',5611.47,8.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(64,58,'2012-12-04 11:32:16',1520.2,21.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(65,56,'2015-02-28 07:48:37',355.28,23.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(66,104,'2014-11-26 07:35:46',3195.08,28.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(67,71,'2007-06-08 02:40:54',4166.01,22.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(68,1,'2014-10-06 17:16:40',5747.04,33.4,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(69,7,'2007-09-10 18:10:46',8794.67,31.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(70,44,'2015-07-01 13:00:18',8296.94,32.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(71,45,'2012-08-16 08:33:54',5289.49,2.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(72,38,'2010-12-17 15:25:29',8104.15,31.4,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(73,77,'2015-12-26 07:17:31',2625.79,11.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(74,73,'2015-10-18 16:34:42',3529.61,29.9,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(75,69,'2008-05-10 01:24:49',7352.49,39.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(76,81,'2008-05-18 04:41:54',8410.87,0.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(77,82,'2014-08-26 14:58:29',914.29,16.9,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(78,76,'2009-04-26 21:40:13',7057.38,31.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(79,77,'2017-02-25 03:43:29',7898.94,12.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(80,88,'2008-03-23 18:52:25',9039.43,7.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(81,95,'2015-12-01 23:13:20',7517.05,29.9,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(82,89,'2007-05-16 17:16:06',3468.75,5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(83,101,'2016-08-29 01:26:52',9429.93,39.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(84,107,'2013-07-10 01:04:08',131,6.4,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(85,103,'2010-07-23 00:33:33',3496.96,11.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(86,22,'2014-04-04 03:53:36',5992.65,37.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(87,24,'2014-04-05 01:25:17',6872.44,35,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(88,1,'2007-05-19 06:40:04',147.87,23.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(89,3,'2009-05-23 23:40:38',6741.88,13.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(90,37,'2011-03-03 14:59:30',9522.15,39.4,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(91,36,'2010-01-14 16:25:48',6723.57,16.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(92,35,'2009-07-17 11:49:52',571.36,5.2,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(93,34,'2013-07-05 22:10:59',7469.92,11,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(94,44,'2007-08-10 18:34:10',741.41,17.4,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(95,41,'2012-04-23 08:37:35',7567.43,21.7,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(96,52,'2011-04-05 16:01:12',9971.05,33.6,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(97,80,'2011-02-27 18:14:45',2110.68,2.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(98,68,'2009-07-07 22:45:39',2643.63,38.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(99,70,'2015-03-10 07:23:52',1144.81,20.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(100,80,'2015-10-19 22:12:51',2564.78,34.6,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(101,44,'2012-02-12 08:52:03',6358.43,18.3,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(102,99,'2014-07-05 05:13:06',7619.55,6.5,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(103,100,'2016-09-06 09:49:36',4971.42,22.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(104,95,'2013-05-25 14:36:01',5532.43,26.8,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1),(105,15,'2015-11-25 17:44:42',888.99,2.1,'2017-04-10 18:04:39','ztj0002','0000-00-00 00:00:00','ztj0002',1);
/*!40000 ALTER TABLE `payroll` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`payroll_bi` BEFORE INSERT ON cs_499_tcms.payroll FOR EACH ROW 
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`payroll_bu` BEFORE UPDATE ON cs_499_tcms.payroll FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO payroll_log 
( 
PayrollID,
EmployeeID,
PaymentDate,
Payment,
HoursWorked,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.PayrollID,
OLD.EmployeeID,
OLD.PaymentDate,
OLD.Payment,
OLD.HoursWorked,
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`payroll_bd` BEFORE DELETE ON cs_499_tcms.payroll FOR EACH ROW 
BEGIN 
INSERT INTO payroll_log 
( 
PayrollID,
EmployeeID,
PaymentDate,
Payment,
HoursWorked,
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
OLD.PayrollID,
OLD.EmployeeID,
OLD.PaymentDate,
OLD.Payment,
OLD.HoursWorked,
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