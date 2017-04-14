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
-- Table structure for table `vehicle_log`
--

DROP TABLE IF EXISTS `vehicle_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `vehicle_log` (
  `VehicleID` int(11) DEFAULT NULL,
  `Brand` varchar(20) DEFAULT NULL,
  `Year` year(4) DEFAULT NULL,
  `Model` varchar(20) DEFAULT NULL,
  `VehicleType` int(20) DEFAULT NULL,
  `Capacity` int(11) DEFAULT NULL,
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
-- Dumping data for table `vehicle_log`
--

LOCK TABLES `vehicle_log` WRITE;
/*!40000 ALTER TABLE `vehicle_log` DISABLE KEYS */;
INSERT INTO `vehicle_log` VALUES (1,'Mercedes-Benz',2004,'Actros',4,17500,'2017-03-26 17:43:03','cs_499_tcms','0000-00-00 00:00:00','cs_499_tcms',1,'U','0000-00-00 00:00:00',NULL),(1,'Chevy',2016,'Thing',2,9000,'2017-03-26 17:43:03','cs_499_tcms','2017-03-26 17:52:13','cs_499_tcms',2,'D','2017-03-26 17:52:36','cs_499_tcms'),(2,'Mercedes-Benz',2004,'Actros',4,17500,'2017-03-26 18:28:09','cs_499_tcms','0000-00-00 00:00:00','cs_499_tcms',1,'D','2017-03-26 18:28:09',NULL),(3,'Mercedes-Benz',2004,'Actros',4,17500,'2017-03-26 18:32:14','cs_499_tcms','0000-00-00 00:00:00','cs_499_tcms',1,'D','2017-03-26 18:32:14',NULL);
/*!40000 ALTER TABLE `vehicle_log` ENABLE KEYS */;
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
