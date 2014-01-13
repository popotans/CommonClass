/*
SQLyog Ultimate v11.24 (32 bit)
MySQL - 5.0.27-community-nt : Database - nq
*********************************************************************
*/


/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`nq` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `nq`;

/*Table structure for table `cls` */

DROP TABLE IF EXISTS `cls`;

CREATE TABLE `cls` (
  `idx` int(11) NOT NULL,
  `title` varchar(200) NOT NULL,
  `p1` int(11) NOT NULL,
  `p2` int(11) NOT NULL,
  `disable` int(11) NOT NULL,
  `orderidx` int(11) NOT NULL,
  `SiteID` int(11) NOT NULL,
  `Url` varchar(1000) default NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cls` */

insert  into `cls`(`idx`,`title`,`p1`,`p2`,`disable`,`orderidx`,`SiteID`,`Url`) values (1,'生活家居',0,0,0,1,0,''),(2,'社会财经',0,0,0,1,0,''),(3,'历史文化',0,0,0,1,0,''),(4,'职场社交',0,0,0,1,0,''),(5,'教育学习',0,0,0,1,0,'');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
