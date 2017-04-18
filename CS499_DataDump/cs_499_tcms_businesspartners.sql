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
-- Table structure for table `businesspartners`
--

DROP TABLE IF EXISTS `businesspartners`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `businesspartners` (
  `CompanyID` int(11) NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(100) CHARACTER SET latin1 DEFAULT NULL,
  `Address` varchar(100) CHARACTER SET latin1 DEFAULT NULL,
  `City` varchar(50) CHARACTER SET latin1 DEFAULT NULL,
  `State` varchar(20) CHARACTER SET latin1 DEFAULT NULL,
  `ZipCode` int(5) DEFAULT NULL,
  `PhoneNumber` varchar(10) DEFAULT NULL,
  `CreatedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `CreatedBy` varchar(20) NOT NULL,
  `LastModifiedDate` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `LastModifiedBy` varchar(20) NOT NULL,
  `Version` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`CompanyID`),
  UNIQUE KEY `businesspartners_address` (`Address`,`City`,`State`,`ZipCode`)
) ENGINE=InnoDB AUTO_INCREMENT=43 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `businesspartners`
--

LOCK TABLES `businesspartners` WRITE;
/*!40000 ALTER TABLE `businesspartners` DISABLE KEYS */;
INSERT INTO `businesspartners` VALUES (2,'Other Store','1414 OtherLane','Otherville','CA',42458,'1234567894','2017-03-26 21:53:14','johnsza','2017-03-26 21:54:00','johnsza',2),(3,'Some Store','1414 SomewhereLane','Somewhereville','MA',56569,'4561237879','2017-03-26 21:54:11','johnsza','0000-00-00 00:00:00','johnsza',1),(4,'Walmart Supercenter','8580 Hwy 72 W','Madison','AL',35758,'2567166951','2017-04-04 16:37:13','driver','0000-00-00 00:00:00','driver',1),(5,'Target','8207 HIGHWAY 72 W','Madison','Alabama',35758,'2566905890','2017-04-06 02:04:21','dc0059','0000-00-00 00:00:00','dc0059',1),(6,'UAH','301 Sparkman Drive','Huntsville','Alabama',35899,'2568241000','2017-04-06 02:10:19','dc0059','0000-00-00 00:00:00','dc0059',1),(14,'Target','Valley Bend, 2750 Cart T Jones Dr SE #7','Huntsville','AL',35802,'7617147673','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(15,'Target','Westside Centre, 6275 University Dr','Huntsville','AL',35806,'5068813825','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(16,'Boeing','Redstone Arsenal, 1000 Redstone Gateway SW','Huntsville','AL',35808,'8859279728','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(17,'Walmart','3031 Memorial Pkwy SW','Huntsville','AL',35801,'4504890536','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(18,'Walmart','11610 Memorial Pkwy SW','Huntsville','AL',35803,'6641615980','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(19,'Best Buy','University Plaza, 5850 University Dr NW','Huntsville','AL',35806,'5958594959','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(20,'Federal-Mogul','300 Wagner Dr','Boaz','AL',35957,'6485762801','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(21,'Lockheed Martin','Research Park Apartments 4800 Bradford Blvd NW','Huntsville','AL',35805,'6170261404','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(22,'Walgreens','3997 University Dr NW','Huntsville','AL',35816,'5161685666','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(23,'Walgreens','11399 Memorial Pkwy SW','Huntsville','AL',35803,'7573735262','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(24,'Sanmina-Sci Corp','13000 Memorial Pkwy SW','Huntsville','AL',35803,'6385994990','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(25,'Kmart','104 US-431','Athens','AL',35611,'9864696940','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(26,'Kmart','7200 US-431','Albertville','AL',35951,'1575280110','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(27,'Lowe\'s Home Improvement','7920 Hwy 72 W','Madison','AL',35758,'8953828524','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(28,'Lowe\'s Home Improvement','6584 US Highway 431 South','Owens Cross Roads','AL',35763,'9070012929','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(29,'Home Depot','Hamilton Square Shopping Center, 10012 Memorial Pkwy SW','Huntsville','AL',35803,'3111779161','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(30,'Home Depot','1035 Memorial Pkwy NW','Huntsville','AL',35801,'7855325594','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(31,'Dick\'s Sporting Goods','Valley Bend, 2718 Carl T Jones Dr SE','Huntsville','AL',35802,'6733396178','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(32,'Apple Bridge Street','Bridge Street Town Centre, 320 The Bridge St','Huntsville','AL',35806,'6155394825','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(33,'Staples','11468 US-431','Guntersville','AL',35976,'7174704221','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(34,'At&t','4800 Whitesburg Dr S #102','Huntsville','AL',35801,'6571368035','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(35,'At&t','6886 Governors Drive West, Suite 108','Huntsville','AL',35806,'3185252085','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(36,'CVS','2212 Whitesburg Dr S','Huntsville','AL',35801,'8982951991','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(37,'CVS','Oakwood Shopping Center, 2525 Oakwood Ave NW','Huntsville','AL',35810,'4466909925','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(38,'CVS','801 S Broad St','Scottsboro','AL',35768,'5006320378','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(39,'DENSO Manufacturing','2400 Denso Dr','Athens','TN',37303,'2896301983','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(40,'Goodman Manufacturing Co.','1810 Wilson Pkwy','Fayetteville','TN',37334,'6401360343','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(41,'Vulcan Materials Company','4210 Stringfield Rd','Huntsville','AL',35810,'1982705021','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1),(42,'Mercury Systems, Inc.','555 Sparkman Dr Ste 400','Huntsville','AL',35816,'3576333996','2017-04-10 00:04:43','dc0059','0000-00-00 00:00:00','dc0059',1);
/*!40000 ALTER TABLE `businesspartners` ENABLE KEYS */;
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`businesspartners_bi` BEFORE INSERT ON cs_499_tcms.businesspartners FOR EACH ROW 
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`businesspartners_bu` BEFORE UPDATE ON cs_499_tcms.businesspartners FOR EACH ROW 
BEGIN 
 
SET NEW.VERSION = OLD.VERSION + 1; 
SET NEW.LASTMODIFIEDDATE = CURRENT_TIMESTAMP; 
 
INSERT INTO businesspartners_log 
( 
CompanyID,
CompanyName,
Address,
City,
State,
ZipCode,
PhoneNumber,
CreatedDate,
CreatedBy,
LastModifiedDate,
LastModifiedBy,
Version,
ModifiedStatus 
) 
VALUES 
( 
OLD.CompanyID,
OLD.CompanyName,
OLD.Address,
OLD.City,
OLD.State,
OLD.ZipCode,
OLD.PhoneNumber,
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
/*!50003 CREATE*/ /*!50017 DEFINER=`cs499`@`%`*/ /*!50003 TRIGGER `cs_499_tcms`.`businesspartners_bd` BEFORE DELETE ON cs_499_tcms.businesspartners FOR EACH ROW 
BEGIN 
INSERT INTO businesspartners_log 
( 
CompanyID,
CompanyName,
Address,
City,
State,
ZipCode,
PhoneNumber,
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
OLD.CompanyID,
OLD.CompanyName,
OLD.Address,
OLD.City,
OLD.State,
OLD.ZipCode,
OLD.PhoneNumber,
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
