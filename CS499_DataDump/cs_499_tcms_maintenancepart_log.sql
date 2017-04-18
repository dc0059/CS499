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
-- Table structure for table `maintenancepart_log`
--

DROP TABLE IF EXISTS `maintenancepart_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `maintenancepart_log` (
  `MaintenancePartID` int(11) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `MaintenanceRecordID` int(11) DEFAULT NULL,
  `PartID` int(11) DEFAULT NULL,
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
-- Dumping data for table `maintenancepart_log`
--

LOCK TABLES `maintenancepart_log` WRITE;
/*!40000 ALTER TABLE `maintenancepart_log` DISABLE KEYS */;
INSERT INTO `maintenancepart_log` VALUES (1,1,2,2,'2017-03-27 17:25:58','johnsza','0000-00-00 00:00:00','johnsza',1,'U','0000-00-00 00:00:00',NULL),(1,200,2,2,'2017-03-27 17:25:58','johnsza','2017-03-27 17:34:22','johnsza',2,'D','2017-03-27 17:35:41','johnsza'),(2,1,2,2,'2017-03-27 20:27:56','johnsza','0000-00-00 00:00:00','johnsza',1,'D','2017-03-27 20:30:49','johnsza'),(3,2,2,4,'2017-03-30 22:28:08','dc0059','0000-00-00 00:00:00','dc0059',1,'U','0000-00-00 00:00:00',NULL),(4,2,3,4,'2017-04-09 21:59:39','dc0059','0000-00-00 00:00:00','dc0059',1,'U','0000-00-00 00:00:00',NULL);
/*!40000 ALTER TABLE `maintenancepart_log` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-04-14  2:15:27
