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
-- Table structure for table `purchaseitems_log`
--

DROP TABLE IF EXISTS `purchaseitems_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `purchaseitems_log` (
  `ItemID` int(11) DEFAULT NULL,
  `OrderID` int(11) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `PartID` int(11) DEFAULT NULL,
  `PartStatus` varchar(50) DEFAULT 'Shipped',
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
-- Dumping data for table `purchaseitems_log`
--

LOCK TABLES `purchaseitems_log` WRITE;
/*!40000 ALTER TABLE `purchaseitems_log` DISABLE KEYS */;
INSERT INTO `purchaseitems_log` VALUES (1,4,3,2,'Shipped','2017-03-26 22:24:46','johnsza','0000-00-00 00:00:00','johnsza',1,'U','0000-00-00 00:00:00',NULL),(1,4,250,2,'Shipped','2017-03-26 22:24:46','johnsza','2017-03-26 22:39:41','johnsza',2,'D','2017-03-26 22:40:44','johnsza'),(2,4,3,2,'Shipped','2017-03-27 20:01:54','johnsza','0000-00-00 00:00:00','johnsza',1,'D','2017-03-27 20:08:56','johnsza'),(4,4,3,2,'Shipped','2017-03-27 20:18:26','johnsza','0000-00-00 00:00:00','johnsza',1,'D','2017-03-27 20:21:38',NULL);
/*!40000 ALTER TABLE `purchaseitems_log` ENABLE KEYS */;
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
