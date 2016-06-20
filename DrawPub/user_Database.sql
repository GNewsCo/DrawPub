/*
SQLyog Enterprise v9.33 GA
MySQL - 5.5.33 : Database - conceptmatch_user
*********************************************************************
*/


/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`DrawPub_user` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `DrawPub_user`;

/*Table structure for table `client` */

DROP TABLE IF EXISTS `client`;

CREATE TABLE `client` (
  `Id` VARCHAR(100) NOT NULL,
  `Secret` VARCHAR(300) DEFAULT NULL,
  `Name` VARCHAR(100) DEFAULT NULL,
  `ApplicationType` TINYINT(4) DEFAULT NULL,
  `Active` TINYINT(4) DEFAULT NULL,
  `RefreshTokenLifeTime` INT(11) DEFAULT NULL,
  `AllowedOrigin` VARCHAR(100) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=INNODB DEFAULT CHARSET=utf8;

/*Table structure for table `refreshtoken` */

DROP TABLE IF EXISTS `refreshtoken`;

CREATE TABLE `refreshtoken` (
  `Id` VARCHAR(250) NOT NULL,
  `subject` VARCHAR(50) DEFAULT NULL,
  `ClientId` VARCHAR(50) DEFAULT NULL,
  `IssuedUtc` datetime DEFAULT NULL,
  `ExpiresUtc` datetime DEFAULT NULL,
  `ProtectedTicket` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `UserId` int(11) NOT NULL,
  `Firstname` varchar(200) NOT NULL,
  `Surname` varchar(200) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `ModifiedDate` datetime NOT NULL,
  `CreatedUserId` int(11) NOT NULL,
  `ModifiedUserId` int(11) NOT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Table structure for table `usercredential` */

DROP TABLE IF EXISTS `usercredential`;

CREATE TABLE `usercredential` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `CreatedById` int(11) NOT NULL DEFAULT '1',
  `CreatedDate` datetime NOT NULL,
  `ModifiedById` int(11) DEFAULT NULL,
  `ModifiedDate` datetime DEFAULT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

/*Table structure for table `userrole` */

DROP TABLE IF EXISTS `userrole`;

CREATE TABLE `userrole` (
  `UserId` int(11) NOT NULL,
  `Role` varchar(100) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `ModifiedDate` datetime NOT NULL,
  `CreatedById` int(11) NOT NULL,
  `ModifiedById` int(11) NOT NULL,
  PRIMARY KEY (`UserId`,`Role`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
