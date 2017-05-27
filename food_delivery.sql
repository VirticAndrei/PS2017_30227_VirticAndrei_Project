CREATE DATABASE  IF NOT EXISTS `food_delivery` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `food_delivery`;
-- MySQL dump 10.13  Distrib 5.7.12, for Win32 (AMD64)
--
-- Host: localhost    Database: food_delivery
-- ------------------------------------------------------
-- Server version	5.7.14-log

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
-- Table structure for table `address`
--

DROP TABLE IF EXISTS `address`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `address` (
  `idaddress` int(11) NOT NULL AUTO_INCREMENT,
  `line1` varchar(45) NOT NULL,
  `line2` varchar(45) DEFAULT NULL,
  `line3` varchar(45) DEFAULT NULL,
  `city` varchar(45) NOT NULL,
  `postcode` varchar(45) DEFAULT NULL,
  `state` varchar(45) NOT NULL,
  PRIMARY KEY (`idaddress`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `address`
--

LOCK TABLES `address` WRITE;
/*!40000 ALTER TABLE `address` DISABLE KEYS */;
INSERT INTO `address` VALUES (1,'strada Artarilor','nr 28 sc B','','Bistrita','420136','Bistrita-Nasaud'),(2,'','','','','',''),(3,'','','','','',''),(4,'','','','','',''),(5,'','','','','',''),(6,'','','','','',''),(7,'','','','','','');
/*!40000 ALTER TABLE `address` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `cart`
--

DROP TABLE IF EXISTS `cart`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `cart` (
  `idcart` int(11) NOT NULL AUTO_INCREMENT,
  `client_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `total` double NOT NULL,
  PRIMARY KEY (`idcart`),
  KEY `client_idx` (`client_id`),
  KEY `product_idx` (`product_id`),
  CONSTRAINT `client` FOREIGN KEY (`client_id`) REFERENCES `client` (`idclient`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `product` FOREIGN KEY (`product_id`) REFERENCES `product` (`idproduct`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `cart`
--

LOCK TABLES `cart` WRITE;
/*!40000 ALTER TABLE `cart` DISABLE KEYS */;
INSERT INTO `cart` VALUES (4,5,13,1,21),(5,5,14,1,19);
/*!40000 ALTER TABLE `cart` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `client`
--

DROP TABLE IF EXISTS `client`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `client` (
  `idclient` int(11) NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(100) NOT NULL,
  `first_name` varchar(45) DEFAULT NULL,
  `last_name` varchar(45) DEFAULT NULL,
  `email` varchar(45) NOT NULL,
  `phone` varchar(45) DEFAULT NULL,
  `address_id` int(11) NOT NULL,
  `payment_id` int(11) NOT NULL,
  PRIMARY KEY (`idclient`),
  UNIQUE KEY `email_UNIQUE` (`email`),
  UNIQUE KEY `username_UNIQUE` (`username`),
  KEY `address_key_idx` (`address_id`),
  KEY `payment_key_idx` (`payment_id`),
  CONSTRAINT `address_key` FOREIGN KEY (`address_id`) REFERENCES `address` (`idaddress`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `payment_key` FOREIGN KEY (`payment_id`) REFERENCES `payment` (`idpayment`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `client`
--

LOCK TABLES `client` WRITE;
/*!40000 ALTER TABLE `client` DISABLE KEYS */;
INSERT INTO `client` VALUES (1,'Andrei.Virtic','41b5f3ba46fb37a22189d21891c1597586c4775274f9c236fea0f33a4a3af186','Andrei','Virtic','andrey_valy93@yahoo.com','0742760192',1,1),(2,'Marcel.Roca','1f7d8737f70b022f518d7181ec439a24f09ca41df0b9a0cfc8b2622da537a10e','Marcel','Roca','marcel_roca@gmail.ro','0753063891',2,2),(3,'Alina.Cosma','b321d383b840cb717b8ed7c05f6c1fb22155cf8b508023f56e12761498d8fa9e','Alina','Cosma','alina.cosma@yahoo.com','0748763190',3,3),(4,'Marian.Rusu','fe268b3259240bd47ff4b18e412cdb6d3e451e87f9e88dcfaf9c926d8e8b99a6','Marian','Rusu','marian.rusu@yahoo.com','',5,5),(5,'Alexandru.Popa','fcde997e058e4eb59665a77a17977212808d5472930a0e57f74f51391bffdfbf','Alexandru','Popa','popa_alex@yahoo.com','',7,7);
/*!40000 ALTER TABLE `client` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `order_product`
--

DROP TABLE IF EXISTS `order_product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `order_product` (
  `idorder_product` int(10) NOT NULL AUTO_INCREMENT,
  `product_id` int(10) NOT NULL,
  `order_id` int(10) NOT NULL,
  `quantity` int(4) NOT NULL,
  `price` double NOT NULL,
  PRIMARY KEY (`idorder_product`),
  KEY `product_key` (`product_id`),
  KEY `order_key` (`order_id`),
  CONSTRAINT `order_key` FOREIGN KEY (`order_id`) REFERENCES `orders` (`idorder`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `product_key` FOREIGN KEY (`product_id`) REFERENCES `product` (`idproduct`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=67 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `order_product`
--

LOCK TABLES `order_product` WRITE;
/*!40000 ALTER TABLE `order_product` DISABLE KEYS */;
INSERT INTO `order_product` VALUES (6,4,1,2,13.5),(7,2,1,1,12),(8,1,1,1,5),(9,2,2,1,12),(10,7,2,1,17),(11,7,3,1,17),(12,6,3,1,17),(13,2,4,1,12),(14,4,4,1,13.5),(15,5,4,1,14),(16,2,5,1,12),(17,4,5,1,13.5),(21,4,6,1,13.5),(22,2,6,1,12),(23,1,6,1,5),(24,4,7,1,13.5),(25,2,7,1,12),(26,1,7,1,5),(27,1,8,1,5),(28,2,8,1,12),(29,4,8,1,13.5),(30,2,9,1,12),(31,4,9,1,13.5),(32,1,9,1,5),(33,1,10,1,5),(34,2,10,1,12),(35,4,10,1,13.5),(36,2,11,1,12),(37,4,11,1,13.5),(38,1,11,1,5),(39,1,12,1,5),(40,2,12,1,12),(41,4,12,1,13.5),(42,1,13,1,5),(43,2,13,1,12),(44,4,13,1,13.5),(45,4,14,1,13.5),(46,2,14,1,12),(47,1,14,1,5),(48,7,14,1,17),(49,1,15,1,5),(50,2,15,1,12),(51,4,15,1,13.5),(52,2,16,1,12),(53,4,16,1,13.5),(54,1,17,1,5),(55,2,17,1,12),(56,4,17,1,13.5),(57,2,18,1,12),(58,1,18,1,5),(59,4,19,3,13.5),(60,2,19,1,12),(61,2,20,3,12),(62,4,20,2,13.5),(63,5,21,1,14),(64,6,21,1,17),(65,2,22,1,12),(66,99,22,1,4.5);
/*!40000 ALTER TABLE `order_product` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orders` (
  `idorder` int(10) NOT NULL AUTO_INCREMENT,
  `client_id` int(10) NOT NULL,
  `total` float NOT NULL,
  `order_date` varchar(20) NOT NULL,
  `order_status` varchar(20) NOT NULL,
  `name` varchar(45) NOT NULL,
  `address` varchar(200) NOT NULL,
  `phone` varchar(45) NOT NULL,
  PRIMARY KEY (`idorder`),
  KEY `client_key` (`client_id`),
  CONSTRAINT `client_key` FOREIGN KEY (`client_id`) REFERENCES `client` (`idclient`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (1,1,44,'5/23/2017 13:42:49','Delivered','','',''),(2,1,29,'5/23/2017 14:40:08','Delivered','','',''),(3,1,34,'5/23/2017 15:06:54','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(4,1,39.5,'5/23/2017 16:29:47','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(5,1,25.5,'5/23/2017 16:37:22','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(6,1,30.5,'5/23/2017 16:46:23','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(7,1,30.5,'5/23/2017 16:52:10','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(8,1,30.5,'5/23/2017 16:54:52','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(9,1,30.5,'5/23/2017 17:00:10','Processing','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(10,1,30.5,'5/23/2017 17:01:36','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(11,1,30.5,'5/23/2017 17:05:51','Processing','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(12,1,30.5,'5/23/2017 17:12:15','Processing','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(13,1,30.5,'5/23/2017 17:26:03','Processing','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(14,1,47.5,'5/23/2017 17:30:35','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(15,1,30.5,'5/23/2017 17:35:31','Placed','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(16,1,25.5,'5/23/2017 17:37:43','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(17,1,30.5,'5/23/2017 17:42:05','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(18,1,17,'5/23/2017 17:45:57','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(19,1,52.5,'5/24/2017 14:53:08','Delivered','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(20,1,63,'5/26/2017 07:50:43','Placed','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(21,1,31,'5/26/2017 07:58:21','Placed','Andrei Virtic','strada Artarilor\r\nnr 28 sc B\r\n\r\nBistrita\r\n420136\r\nBistrita-Nasaud','0742760192'),(22,1,16.5,'5/26/2017 08:22:13','Processing','Andrei Virtic','strada Dorobantilor\r\nnr 120\r\n\r\nCluj-Napoca\r\n420136\r\nCluj','0742760192');
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment`
--

DROP TABLE IF EXISTS `payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment` (
  `idpayment` int(11) NOT NULL AUTO_INCREMENT,
  `card_number` varchar(17) NOT NULL,
  `holder_name` varchar(45) NOT NULL,
  `exp_date` varchar(5) NOT NULL,
  `security_code` int(3) NOT NULL,
  PRIMARY KEY (`idpayment`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment`
--

LOCK TABLES `payment` WRITE;
/*!40000 ALTER TABLE `payment` DISABLE KEYS */;
INSERT INTO `payment` VALUES (1,'9456012365781093','Andrei Virtic','0919',134),(2,'','','',0),(3,'','','',0),(4,'','','',0),(5,'','','',0),(6,'','','',0),(7,'','','',0);
/*!40000 ALTER TABLE `payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `product`
--

DROP TABLE IF EXISTS `product`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `product` (
  `idproduct` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `price` float NOT NULL,
  `description` varchar(200) DEFAULT NULL,
  `category` varchar(45) NOT NULL,
  `grams` int(5) DEFAULT NULL,
  PRIMARY KEY (`idproduct`)
) ENGINE=InnoDB AUTO_INCREMENT=107 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `product`
--

LOCK TABLES `product` WRITE;
/*!40000 ALTER TABLE `product` DISABLE KEYS */;
INSERT INTO `product` VALUES (1,'Cartofi prajiti',5,'cartofi prajiti in ulei  cu sare','Garnituri',200),(2,'Papanasi',12,'Papanasi cu crema de iaurt si sirop de zmeura','Desert',150),(4,'Piep de pui la gratar',13.5,'piept de pui prajit pe gratar','Fel principal',140),(5,'Salata Fresh',14,'mixt salatã - rosii cherry - mãsline - otet balsamic - brânzã feta - ulei masline + dressing','Salate',350),(6,'Salata Fresh cu Pui',17,'mixt salatã - rosii cherry - mãsline - cascaval - otet balsamic - porumb - piept de pui la grãtar + dressing','Salate',430),(7,'Salata Fresh cu Prosciutto',17,'mixt salatã - rosii cherry - mãsline - brânzã feta - otet balsamic - prosciutto crudo - ulei masline + dressing','Salate',400),(8,'Salata Fresh cu Ton',17,'mixt salatã - rosii cherry - mãsline - brânza feta - otet balsamic - lamâie stoarsa - ton - porumb - morcov - ceapã - lamâie + dressing','Salate',400),(9,'Salata cu Pui crocant',18,'ardei - rosii - castraveti - telemea - cascaval - salatã iceberg - mãsline - lamâie stoarsa - ulei de masline - piept de pui crocant - sos la alegere','Salate',550),(10,'Salata cu Ton',19,'ton - rosii - castraveti - ardei - cascaval - telemea -salatã iceberg - porumb - 1ou - mãsline - ceapa - ulei de masline - lamâie 1/4 - sos remoulade','Salate',600),(11,'Salata de cruditati',10,'salata iceberg - ardei - rosii - castraveti - porumb - mãsline - lamâie stoarsa  - ulei de masline + otet balsamic','Salate',400),(12,'Pizza Canibale ',21,'Sos de rosii,  costitã,  suncã,  salam picant,  cârnati,  mozzarella,  mãsline','Pizza',640),(13,'Pizza Capriciosa ',21,'Sos de rosii, sunca, ciuperci, mozzarella, capere ','Pizza',645),(14,'Pizza Diavolo ',19,'Sos de rosii, salam picant, mozzarella, ardei gras','Pizza',530),(15,'Pizza Hawaii ',19,'Sos de rosii, porumb, suncã, mozzarella, ananas ','Pizza',540),(16,'Pizza Margherita ',14,'Sos de rosii, oregano, mozzarella ','Pizza',420),(17,'Pizza Nazareth ',21,'Sos de rosii, porumb, pui crocant, ciuperci, mozzarela, ardei gras ','Pizza',700),(18,'Pizza Prosciutto Crudo ',21,'Sos de rosii, mozzarella, prosciutto crudo ','Pizza',560),(19,'Pizza Prosciutto Funghi ',19,'Sos de rosii, sunca, mozzarella, ciuperci','Pizza',490),(20,'Pizza Provinciale ',21,'Sos de rosii, porumb, suncã, costitã, ciuperci, mozzarela, cârnati, ceapã','Pizza',675),(21,'Pizza Quatro Formaggi ',19,'Sos de rosii, Mozzarella, schweizer, gorgonzola, parmezan','Pizza',430),(22,'Pizza Quattro Stagioni  ',21,'Sos de rosii, porumb, sunca, salam picant, ciuperci, mozzarella, ardei gras, masline','Pizza',630),(23,'Pizza cu Ton ',19,'Sos de rosii, ton, mozzarela, ceapã','Pizza',510),(24,'Pizza Vegetariana ',19,'Sos de rosii, porumb, ciuperci, ardei gras, mozzarella, sau cascaval vegetal, rosii, masline','Pizza',515),(25,'Pasta integrale alle verdure ',21,'Paste integrale (fusilli), dovlecel, vânata, broccoli, ciuperci champignon, rosii cherry, ceapa,usturoi, busuioc, patrunjel, parmezan','Paste',450),(26,'Spaghetti and Meatballs  ',18,'Spaghetti, chiftelute preparate dupa reteta proprie, sos de rosii, parmezan','Paste',450),(27,'Garden risotto  ',21,'Orez, broccoli, rosii cherry, ciuperci champignion, dovlecel, morcov, ceapa, usturoi, smântâna, vin, patrunjel','Paste',450),(28,'Paste Carbonara ',21,'tagliatelle, bacon, ceapa, usturoi, vin, nucsoara, smantana, ou, parmezan, sare, piper','Paste',450),(29,'Paste Quatro Formaggi ',22,'penne, emmentaller, gorgonzola, brie, smantana dulce, sare, piper, parmezan','Paste',450),(30,'Paste Bolognese ',19,'spaghetti, sos ragu, parmezan ','Paste',450),(31,'Paste Polo e Funghi ',20,'penne, pui, ciuperci, smantana dulce, gorgonzola, busuioc, sare, piper, parmezan \r\n','Paste',450),(32,'Simple cream Cheesecake ',8.5,'Simple cream Cheesecake \r\n','Desert',200),(33,'Tarta cu branza  ',7.5,'cu un gust fin de braza dulce si o textura cremoasa asortata cu un sos de fructe de padure','Desert',250),(34,'Tarta cu mere ',7.5,'Tarta delicioasa cu mere, scortisoara, stafide - asortata cu un sos unic de vanilie si caramel','Desert',250),(35,'Clatite cu Finetti ',6,'oua, lapte, faina, crema de finneti','Desert',200),(36,'Clatite cu Gem',6,'oua, lapte, faina, gem de capsuni','Desert',200),(37,'Salata Asortata de Varza ',4.5,'Salata de varza, rosii','Garnituri',200),(38,'Salata Asortata ',4.5,'rosii, castraveti, salatã iceberg ','Garnituri',280),(39,'Orez cu Legume ',5,'Orez cu legume ','Garnituri',270),(40,'Blue Cheese Burger',19.99,'chifla, carne de vita, gorgonzola, rucola, rosii, maioneza','Burger',300),(41,'Meniu Blue Cheese Burger',27,'chifla, carne de vita, gorgonzola, rucola, rosii, maioneza, inele de ceapa pane, cartofi wedges, salata coleslaw, ketchup la plic','Burger',600),(42,'Cheesy Burger',18,'chifla, carne de vita, cascaval, salata verde, rosii, ceapa, castraveti murati, mustar','Burger',300),(43,'Meniu Cheesy Burger',26,'chifla, carne de vita, cascaval, salata verde, rosii, ceapa, castraveti murati, mustar, ketchup, inele de ceapa pane, cartofi wedges, salata coleslaw','Burger',600),(44,'American Burger',17,'chifla, carne de vita, salata verde, rosii, ceapa, maioneza','Burger',300),(45,'Meniu American Burger',25,'chifla, carne de vita, salata verde, rosii, ceapa, maioneza, ketchup, cartofi wedges, inele de ceapa pane, salata coleslaw, ketchup','Burger',600),(46,'L.A. Burger',18,'chifla, carne de vita, guacamole, cascaval, rosii, salata','Burger',300),(47,'Meniu L.A. Burger',27,'chifla, carne de vita, guacamole, cascaval, rosii, salata, inele de ceapa pane, cartofi wedges, salata coleslaw, ketchup','Burger',550),(48,'Crispy Burger',18,'chifla, fâşii din piept de pui marinat, în crustă crocantă de pesmet panko, bacon prajit, rosii, salata, cascaval, sos de usturoi','Burger',300),(49,'Sos de Rosii Dulce',3,'rosii pasate, condimente','Sosuri',80),(50,'Sos de Rosii Picant',3,'rosii pasate, condimente','Sosuri',80),(51,'Sos de Usturoi',3.5,'usturoi, smantana, maioneza, condimente','Sosuri',80),(52,'Sos Sriracha Hot Chilli',3.5,'Chilli, usturoi, zahăr, oţet, sare','Sosuri',30),(53,'Smantana de caju',4,'caju, apa, condimente','Sosuri',80),(54,'Sos Feta',3,'iaurt, feta, lamaie, usturoi, busuioc','Sosuri',80),(55,'Sriracha Mayo',3,'Maioneză, chilli, usturoi, zahăr, oţet, sare.','Sosuri',80),(56,'Sos Caesar',3,'maioneza, ansoa, capere, condimente','Sosuri',80),(57,'Sos Veggie cheese',3.5,'lapte de cocos, ardei, morcovi, tofu, condimente','Sosuri',80),(58,'Sos Tzatziki',3,'iaurt, castraveti, usturoi, condimente','Sosuri',80),(59,'Sos Thousand Island',3,'maioneza, sos de rosii, castraveti murati, condimente','Sosuri',80),(60,'Supa Crema de Broccoli',11,'broccoli, dovlecel, cartofi, ceapa, parmezan, smantana dulce, crutoane','Supe',350),(61,'Supa crema de Linte',11,'linte, lapte de cocos, zahar de cocos, unt de cocos, sos de rosii, peperoncino, coraindru, telina, morcovi, ceapa, usturoi','Supe',350),(62,'Supa crema de Dovleac',11,'dovleac, naut, lapte cocos, otet mere, coriandru, nucsoara, crutoane','Supe',350),(63,'Supa crema de Hribi',14,'hribi, ciuperci champignon,lapte de cocos, pastarnac, crutoane incluse','Supe',350),(64,'Ciorba de Pui a la Grec',11,'nu e inclusa smantana extra, chifla, ardei iute','Supe',400),(65,'Costite caramelizate in sos barbeque',27,'Costiţe de porc marinate peste noapte şi gătite lent la cuptor, caramelizate apoi în sos barbeque, servite cu cartofi wedges','Fel principal',300),(66,'Ciolan la cuptor cu varza rosie calita',32,'Ciolan de porc (fără şorici), gătit la cuptor cu ierburi aromate, servit cu varză roşie călită cu stafide şi cartofi wedges','Fel principal',400),(67,'Meniu Cascaval Pane',20,'cascaval pane, garnitura la alegere: cartofi prajiti,cartofi wedges,orez simplu,orez cu legume, salata varza','Fel principal',200),(68,'Pui Tikka Masala',22,'cubulete de piept de pui gatite in sos tikka masala(sos de rosii, ceapa, condimente, lapte de cocos)','Fel principal',300),(69,'Meniu Schnitzel vienez',25,'Schnitzel din carne de vită în crustă crocantă de pesmet, servit cu cartofi wedges şi gem de merişoare','Fel principal',200),(70,'Costite caramelizate in sos sweet chilli',27,'Costiţe de porc marinate peste noapte şi gătite lent la cuptor, caramelizate apoi în sos sweet chilli dulce acrişor, servite cu cartofi wedges','Fel principal',300),(71,'Meniu File de Salau la Gratar',25,'file de salau la gratar, garnitura la alegere: cartofi prajiti,cartofi wedges,orez simplu,orez cu legume','Fel principal',150),(72,'Chilli con carne',21,'carne de vita tocata, fasole rosie, porumb, sos de rosii, ardei, ceapa, usturoi, chimen, peperoncino, smantana, focaccia','Fel principal',350),(73,'Meniu Rulada de pui invelita in bacon',25,'piept de pui, ciuperci, ardei, cascaval gouda, bacon, sos parmezan, garnitura la alegere: cartofi prajiti/cartofi wedges/orez simplu/orez cu legume','Fel principal',200),(74,'Meniu File de Pastrav la Gratar',25,'file de pastrav la gratar, garnitura la alegere: cartofi prajiti/cartofi wedges/orez simplu/orez cu legume','Fel principal',130),(75,'Bruschete cu hribi si cascaval',15,'paine, hribi, cascaval, usturoi, ulei','Aperitive',200),(76,'Bruschete cu rosii',8.9,'paine prajita, rosii, busuioc, usturoi','Aperitive',200),(77,'Bruschete cu ton',8.9,'paine prajita, ton, ceapa, masline, lamaie','Aperitive',200),(78,'Cascaval pane',14,'cas, ou, pesmet, susan','Aperitive',200),(79,'Clatite cu piept de pui ',17,'2 clatite, piept de pui, ceapa, ardei, smantana, condimente','Aperitive',250),(80,'Rulou de somon fume',18.5,'somon fume, pasta branza dulce, salata verde, sweet chili, susan','Aperitive',250),(81,'Trio crostini formaggi',18,'paine prajita, mozzarella, prosciutto, somon fume','Aperitive',300),(82,'Dorada regala',13,'dorada, condimente, lamaie','Peste',100),(83,'FIle de salau cu rosii si capere',23.5,'salau, rosii chery, capere, usturoi, lamaie, sare, piper','Peste',250),(84,'Pastrav in crusta de malai',12,'pastrav, ou, faina, faina de malai, lamaie','Peste',100),(85,'Pastrav la gratar',10,'pastrav, condimente, legume','Peste',100),(86,'Salau grill',10,'salau, condimente, lamaie','Peste',100),(87,'Salau in fulgi de porumb',11,'salau, condimente, ou, fulgi de porumb, lamaie','Peste',100),(88,'Somon la gratar',14,'somon, condimente','Peste',100),(89,'Somon mediteranean',27,'somon, rosii, usturoi, capere, piper, ardei iute, patrunjel','Peste',250),(90,'Ciorba de fasole cu ciolan',11,'Ciorba de fasole cu ciolan 60g/340ml','Supe',340),(91,'Ciorba de Burta',13,'ciorba de burta 80g / 320 ml','Supe',320),(92,'Ciorba taraneasca de vacuta ',12,'Ciorba taraneasca de vacuta 60g/340ml','Supe',340),(93,'Cappy ',4.5,'Bautura racoritoare necarbogazoasa','Bauturi',330),(94,'Cappy Pulpy',4.5,'Bautura racoritoare necarbogazoasa','Bauturi',330),(95,'Cappy Nectar',4.5,'Bautura racoritoare necarbogazoasa','Bauturi',330),(96,'Nestea',4.5,'Bautura racoritoare necarbogazoasa','Bauturi',330),(97,'Freshuri',7,'portocale, grapefruit, morcov, mere, lamaie, mix.','Bauturi',330),(98,'Limonada',8,'fresh de lamaie, zahar, apa plata, menta, lime ','Bauturi',500),(99,'Coca Cola',4.5,'bautura racoritoare carbogazoasa','Bauturi',500),(100,'Coca Cola Zero',4.5,'bautura racoritoare carbogazoasa','Bauturi',500),(101,'Fanta Portocale',4.5,'bautura racoritoare carbogazoasa','Bauturi',500),(102,'Fanta Madness',4.5,'bautura racoritoare carbogazoasa','Bauturi',500),(103,'Sprite',4.5,'bautura racoritoare carbogazoasa','Bauturi',500),(104,'Schweppes',4.5,'bautura racoritoare carbogazoasa','Bauturi',500),(105,'Dorna apa plata',3.5,'apa plata dorna','Bauturi',500),(106,'Dorna apa minerala',3.5,'apa minerala dorna','Bauturi',500);
/*!40000 ALTER TABLE `product` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2017-05-27 15:30:38
