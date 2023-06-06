# Criação da base
CREATE DATABASE pim_6;

# Selecionando base para utilização
USE pim_6;

# Criando as tabelas

CREATE TABLE `tipoacesso` 
(
  `codigo` int(11) NOT NULL AUTO_INCREMENT,
  `descricao` varchar(15) NOT NULL,
  PRIMARY KEY (`codigo`)
);

CREATE TABLE `usuario` (
  `codigo` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) NOT NULL,
  `data_cadastro` date NOT NULL,
  `login` varchar(50) NOT NULL,
  `senha` varchar(25) NOT NULL,
  `data_atualizacao` datetime DEFAULT NULL,
  `tipo_acesso` int(11) DEFAULT 2,
  PRIMARY KEY (`codigo`),
  KEY `usuario_ibfk_1` (`tipo_acesso`),
  CONSTRAINT `usuario_ibfk_1` FOREIGN KEY (`tipo_acesso`) REFERENCES `tipoacesso` (`codigo`)
);

CREATE TABLE `cliente` 
(
  `codigo` int(11) NOT NULL AUTO_INCREMENT,
  `rg` varchar(9) NOT NULL,
  `cpf` varchar(14) NOT NULL,
  `nome` varchar(50) NOT NULL,
  `data_cadastro` date NOT NULL,
  `endereco` varchar(50) NOT NULL,
  `telefone` varchar(15) NOT NULL,
  `email` varchar(50) NOT NULL,
  PRIMARY KEY (`codigo`)
);

CREATE TABLE `produto` 
(
  `codigo` int(11) NOT NULL AUTO_INCREMENT,
  `codigo_barras` varchar(255) NOT NULL,
  `nome` varchar(50) NOT NULL,
  `categoria` varchar(50) NOT NULL,
  `fabricante` varchar(50) NOT NULL,
  `quantidade` int(11) NOT NULL,
  `valor_produto` decimal(8,2) NOT NULL,
  `plataforma` varchar(50) NOT NULL,
  `prazo_garantia` int(11) NOT NULL,
  `data_cadastro` date NOT NULL,
  `data_atualizacao` datetime DEFAULT NULL,
  PRIMARY KEY (`codigo`)
);

CREATE TABLE `venda` 
(
  `codigo` int(11) NOT NULL AUTO_INCREMENT,
  `codigo_cliente` int(11) NOT NULL,
  `data` datetime NOT NULL,
  `status` varchar(15) DEFAULT NULL,
  PRIMARY KEY (`codigo`)
);

CREATE TABLE `item_venda` 
(
  `codigo_venda` int(11) NOT NULL,
  `codigo_produto` int(11) NOT NULL,
  `quatidade` int(11) NOT NULL,
  KEY `codigo_venda` (`codigo_venda`),
  KEY `codigo_produto` (`codigo_produto`),
  CONSTRAINT `item_venda_ibfk_1` FOREIGN KEY (`codigo_venda`) REFERENCES `venda` (`codigo`),
  CONSTRAINT `item_venda_ibfk_2` FOREIGN KEY (`codigo_produto`) REFERENCES `produto` (`codigo`)
);

# Criando as Procedures / Funções

DELIMITER $$
CREATE PROCEDURE `Atualizarcliente`(
	p_codigo int,
	p_rg varchar(9),
	p_cpf varchar(14) ,
	p_nome varchar(50) ,
	p_endereco varchar(50) ,
	p_telefone int,
	p_email varchar(50)

)
begin

	update 
		cliente
	set
		rg = p_rg,
        cpf = p_cpf,
        nome = p_nome,
        endereco = p_endereco,
        telefone = p_telefone,
        email = p_email
	where 
		codigo = p_codigo;
	
    select row_count() as linhasafetadas;
	

end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Deletacliente`(
	p_codigo int
)
begin

	delete from
		cliente
	where
		codigo = (p_codigo);
        
	select row_count() as Linhasafetadas;
    
end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Deletaitemvenda`(
	p_codigo int
)
begin

	delete from
		item_venda
	where
		codigo = p_codigo;
        
        
	select row_count() as Linhasafetadas;
    
end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Deletaproduto`(
	p_codigo int
)
begin

	delete from
		produto
	where
		codigo = p_codigo;        
        
	select row_count() as Linhasafetadas;
    
end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Deletausuario`(
	p_codigo int
)
begin

	delete from
		usuario
	where
		codigo = (p_codigo);
        
	select row_count() as Linhasafetadas;
    
end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Deletavenda`(
	p_codigo int
)
begin

	delete from
		item_venda
	where
		codigo_venda = p_codigo;
        
	delete from
		venda
	where
		codigo = (p_codigo);
        
	select row_count() as Linhasafetadas;
    
end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Inserirclientes`(
	p_rg varchar(9),
	p_cpf varchar(14) ,
	p_nome varchar(50) ,
	p_endereco varchar(50) ,
	p_telefone int,
	p_email varchar(50) 
)
begin 

	insert into
		cliente
        (rg, cpf, nome, data_cadastro, endereco, telefone, email)
	values
		(p_rg, p_cpf, p_nome, date(now()), p_endereco, p_telefone, p_email);
        
	select last_insert_id() as codigo ;

end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Inserirproduto`(
	p_codigo_barras varchar(255),
    p_nome varchar(50),
    p_categoria varchar(50),
    p_fabricante varchar(50),
    p_quantidade int(11),
    p_valor_produto decimal (8,2),
    p_plataforma varchar(50),
    p_prazo_garantia int(11)
)
begin

	insert into 
		produto
		(codigo_barras, nome, categoria, fabricante, quantidade, valor_produto, plataforma, prazo_garantia, data_cadastro)    
    values
		(p_codigo_barras, p_nome, p_categoria, p_fabricante, p_quantidade, p_valor_produto, p_plataforma, p_prazo_garantia, date(now()));
	
    select last_insert_id() as codigo;
	
end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Inserirusuarios`(
	p_nome varchar (50),
	p_login varchar(50),
	p_senha varchar(25)
)
begin

	insert into 
		usuario
		(nome, data_cadastro, login, senha)
	values 
		 (p_nome, date (now()), p_login, p_senha);
		 
     select last_insert_id() as codigo; 

end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Listarclientes`()
begin 
	
    select
		*
	from
		cliente;

end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Listarprodutos`()
begin

	select 
		*
	from
		produto;

end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Listarusuarios`()
begin

	select
		*
   from 
		usuario as a
        inner join tipoacesso as b on a.tipo_acesso = b.codigo;
    
end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Listarvendas`()
begin

	select
		a.codigo,
        a.codigo_cliente,
        b.nome,
        a.data,
        a.status,
        sum(c.quatidade) as quantidade_vendido,
        sum(d.valor_produto * c.quatidade) as valor_total
	from
		venda as a
        inner join Cliente as b on a.codigo_cliente = b.codigo
        inner join item_venda as c on a.codigo = codigo_venda
        inner join Produto as d on c.codigo_produto = d.codigo
	group by
        a.codigo,
        a.codigo_cliente,
        b.nome,
        a.data,
        a.status;

end$$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `Validalogin`(
	p_login varchar(50),
    p_senha varchar(50)
)
begin

	select
		*
	from
		usuario
	where
		login = p_login
        and senha = p_senha;   
   
end$$
DELIMITER ;