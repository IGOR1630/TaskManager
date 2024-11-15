-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema TaskManager
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema TaskManager
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `TaskManager` DEFAULT CHARACTER SET utf8 ;
USE `TaskManager` ;

-- -----------------------------------------------------
-- Table `TaskManager`.`Tarefas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `TaskManager`.`Tarefas` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `Nome` VARCHAR(200) NOT NULL,
  `Custo` DECIMAL(10,2) NOT NULL,
  `dataLimite` DATE NOT NULL,
  `ordemApresentacao` INT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `nome_UNIQUE` (`Nome` ASC) VISIBLE)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
