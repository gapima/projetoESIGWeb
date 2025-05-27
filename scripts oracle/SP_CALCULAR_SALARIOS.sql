create or replace NONEDITIONABLE PROCEDURE SP_CALCULAR_SALARIOS IS
  -- Variáveis para o loop
  CURSOR cur_pessoas IS
    SELECT id AS pessoa_id,
           nome AS pessoa_nome,
           cargo_id
      FROM pessoa;

  v_base_creditos   NUMBER;
  v_salario_bruto   NUMBER;
  v_descontos       NUMBER;
  v_salario_liquido NUMBER;
BEGIN
  -- Limpa tabela de destino
  DELETE FROM pessoa_salario;

  -- Para cada pessoa, calcula e insere os valores
  FOR rec IN cur_pessoas LOOP

    ------------------------------------------------
    -- 1) Calcular Base de Créditos (Vencimentos tipo C, forma V)
    ------------------------------------------------
    SELECT NVL(SUM(v.valor), 0)
      INTO v_base_creditos
    FROM cargo_vencimentos cv
    JOIN vencimentos       v
      ON v.id = cv.vencimento_id
    WHERE cv.cargo_id          = rec.cargo_id
      AND v.tipo                = 'C'
      AND v.forma_incidencia    = 'V';

    ------------------------------------------------
    -- 2) Calcular Salário Bruto
    ------------------------------------------------
    SELECT NVL(SUM(
           CASE 
             WHEN v.tipo = 'C' AND v.forma_incidencia = 'V' THEN v.valor
             WHEN v.tipo = 'C' AND v.forma_incidencia = 'P' THEN (v.valor/100) * v_base_creditos
             ELSE 0
           END
         ), 0)
      INTO v_salario_bruto
    FROM cargo_vencimentos cv
    JOIN vencimentos       v
      ON v.id = cv.vencimento_id
    WHERE cv.cargo_id = rec.cargo_id;

    ------------------------------------------------
    -- 3) Calcular Descontos
    ------------------------------------------------
    SELECT NVL(SUM(
           CASE 
             WHEN v.tipo = 'D' AND v.forma_incidencia = 'V' THEN v.valor
             WHEN v.tipo = 'D' AND v.forma_incidencia = 'P' THEN (v.valor/100) * v_base_creditos
             ELSE 0
           END
         ), 0)
      INTO v_descontos
    FROM cargo_vencimentos cv
    JOIN vencimentos       v
      ON v.id = cv.vencimento_id
    WHERE cv.cargo_id = rec.cargo_id;

    ------------------------------------------------
    -- 4) Calcular Salário Líquido
    ------------------------------------------------
    v_salario_liquido := v_salario_bruto - v_descontos;

    ------------------------------------------------
    -- 5) Inserir o registro na tabela pessoa_salario
    ------------------------------------------------
    INSERT INTO pessoa_salario (
      pessoa_id,
      nome,
      salario_bruto,
      descontos,
      salario_liquido
    ) VALUES (
      rec.pessoa_id,
      rec.pessoa_nome,
      v_salario_bruto,
      v_descontos,
      v_salario_liquido
    );

  END LOOP;

  -- Grava as mudanças
  COMMIT;
END SP_CALCULAR_SALARIOS;
