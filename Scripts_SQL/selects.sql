-- Exportações realizadas entre 2019 e 2020 agrupadas pelo tipo de natureza dos produtos

select b.codNCM, count ('*'), c.descricaoNCM
from exportacoes a
join sh4 b on b.codSH4 = a.codSH4
join ncm c on c.codNCM = b.codNCM
where ano = 2019
group by b.codNCM; 


-- Exportações realizadas entre 2019 e 2020 agrupadas por países destino dos produtos

select p.descricaoPais, count('*')
from exportacoes as e
join codpais as p on e.pais = p.codPais
where e.ano = 2019
group by p.descricaoPais;

select p.descricaoPais, count('*')
from exportacoes as e
join codpais as p on e.pais = p.codPais
where e.ano = 2020
group by p.descricaoPais;

-- Exportações realizadas entre 2019 e 2020 agrupadas por municípios de origem dos produtos

select p.descricaoMunicipio, e.estado, count('*')
from exportacoes as e
join codmunicipio as p on e.codMunicipio = p.codMunicipio
where e.ano = 2019
group by p.descricaoMunicipio;

select p.descricaoMunicipio, e.estado, count('*')
from exportacoes as e
join codmunicipio as p on e.codMunicipio = p.codMunicipio
where e.ano = 2020
group by p.descricaoMunicipio;







