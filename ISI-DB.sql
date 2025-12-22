--
-- PostgreSQL database dump
--

-- Dumped from database version 15.14 (Debian 15.14-1.pgdg13+1)
-- Dumped by pg_dump version 17.0

-- Started on 2025-12-17 16:16:03

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3468 (class 1262 OID 16384)
-- Name: iotdb; Type: DATABASE; Schema: -; Owner: admin
--

CREATE DATABASE iotdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE iotdb OWNER TO admin;

\connect iotdb

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 214 (class 1259 OID 16433)
-- Name: alise_estabilidade_turnos; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.alise_estabilidade_turnos (
    "Polo" character varying,
    "Turno" character varying,
    "Responsavel" character varying,
    "Instabilidade_Turno" double precision,
    "Nivel_Risco_Instabilidade" character varying
);


ALTER TABLE public.alise_estabilidade_turnos OWNER TO admin;

--
-- TOC entry 218 (class 1259 OID 32928)
-- Name: analise_conforto_horario; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.analise_conforto_horario (
    "Polo" character varying,
    "DataHora" timestamp without time zone,
    "media temperatura" double precision,
    "media humidade" double precision,
    "Indice_Conforto" double precision,
    "Nivel_Conforto" character varying
);


ALTER TABLE public.analise_conforto_horario OWNER TO admin;

--
-- TOC entry 217 (class 1259 OID 32923)
-- Name: analise_estabilidade_turnos; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.analise_estabilidade_turnos (
    "Polo" character varying,
    "Turno" character varying,
    "Responsavel" character varying,
    "Instabilidade_Turno" double precision,
    "Nivel_Risco_Instabilidade" character varying
);


ALTER TABLE public.analise_estabilidade_turnos OWNER TO admin;

--
-- TOC entry 220 (class 1259 OID 41106)
-- Name: app_users; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.app_users (
    id integer NOT NULL,
    username character varying(50) NOT NULL,
    password_hash character varying(255) NOT NULL,
    role character varying(20) DEFAULT 'Operator'::character varying,
    created_at timestamp without time zone DEFAULT now()
);


ALTER TABLE public.app_users OWNER TO admin;

--
-- TOC entry 219 (class 1259 OID 41105)
-- Name: app_users_id_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.app_users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.app_users_id_seq OWNER TO admin;

--
-- TOC entry 3469 (class 0 OID 0)
-- Dependencies: 219
-- Name: app_users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.app_users_id_seq OWNED BY public.app_users.id;


--
-- TOC entry 215 (class 1259 OID 32913)
-- Name: dados_sensores_limpos; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.dados_sensores_limpos (
    "ID" character varying,
    sensor character varying,
    "Tipo" character varying,
    "Polo" character varying,
    "Valor" double precision,
    "Unidade" character varying,
    "Validação" character varying,
    "DataHora" timestamp without time zone,
    "Dia_Semana" character varying,
    "Turno" character varying,
    "Responsavel" character varying
);


ALTER TABLE public.dados_sensores_limpos OWNER TO admin;

--
-- TOC entry 224 (class 1259 OID 41125)
-- Name: machine_logs; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.machine_logs (
    id integer NOT NULL,
    rule_applied_id integer,
    sensor_reading double precision,
    external_temp_context double precision,
    command_issued character varying(100),
    executed_at timestamp without time zone DEFAULT now(),
    status character varying(20) DEFAULT 'PENDING'::character varying
);


ALTER TABLE public.machine_logs OWNER TO admin;

--
-- TOC entry 223 (class 1259 OID 41124)
-- Name: machine_logs_id_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.machine_logs_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.machine_logs_id_seq OWNER TO admin;

--
-- TOC entry 3470 (class 0 OID 0)
-- Dependencies: 223
-- Name: machine_logs_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.machine_logs_id_seq OWNED BY public.machine_logs.id;


--
-- TOC entry 222 (class 1259 OID 41117)
-- Name: machine_rules; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.machine_rules (
    id integer NOT NULL,
    target_sensor_id character varying(50),
    rule_name character varying(100),
    threshold_value double precision,
    condition_type character varying(5),
    action_command character varying(100),
    is_active boolean DEFAULT true
);


ALTER TABLE public.machine_rules OWNER TO admin;

--
-- TOC entry 221 (class 1259 OID 41116)
-- Name: machine_rules_id_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

CREATE SEQUENCE public.machine_rules_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.machine_rules_id_seq OWNER TO admin;

--
-- TOC entry 3471 (class 0 OID 0)
-- Dependencies: 221
-- Name: machine_rules_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: admin
--

ALTER SEQUENCE public.machine_rules_id_seq OWNED BY public.machine_rules.id;


--
-- TOC entry 216 (class 1259 OID 32918)
-- Name: relatorio_clima_comparativo; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.relatorio_clima_comparativo (
    "Polo" character varying,
    "DataHora" timestamp without time zone,
    "Hum_Media" double precision,
    "Temp_Media" double precision,
    "hourly.temperature_2m" double precision,
    "hourly.relative_humidity_2m" integer,
    dif_absoluta_temp double precision,
    dif_hum_absoluta double precision,
    esforco_climatizacao double precision
);


ALTER TABLE public.relatorio_clima_comparativo OWNER TO admin;

--
-- TOC entry 3293 (class 2604 OID 41109)
-- Name: app_users id; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.app_users ALTER COLUMN id SET DEFAULT nextval('public.app_users_id_seq'::regclass);


--
-- TOC entry 3298 (class 2604 OID 41128)
-- Name: machine_logs id; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.machine_logs ALTER COLUMN id SET DEFAULT nextval('public.machine_logs_id_seq'::regclass);


--
-- TOC entry 3296 (class 2604 OID 41120)
-- Name: machine_rules id; Type: DEFAULT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.machine_rules ALTER COLUMN id SET DEFAULT nextval('public.machine_rules_id_seq'::regclass);



--
-- TOC entry 3472 (class 0 OID 0)
-- Dependencies: 219
-- Name: app_users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.app_users_id_seq', 5, true);


--
-- TOC entry 3473 (class 0 OID 0)
-- Dependencies: 223
-- Name: machine_logs_id_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.machine_logs_id_seq', 1, false);


--
-- TOC entry 3474 (class 0 OID 0)
-- Dependencies: 221
-- Name: machine_rules_id_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.machine_rules_id_seq', 1, true);


--
-- TOC entry 3302 (class 2606 OID 41113)
-- Name: app_users app_users_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.app_users
    ADD CONSTRAINT app_users_pkey PRIMARY KEY (id);


--
-- TOC entry 3304 (class 2606 OID 41115)
-- Name: app_users app_users_username_key; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.app_users
    ADD CONSTRAINT app_users_username_key UNIQUE (username);


--
-- TOC entry 3308 (class 2606 OID 41132)
-- Name: machine_logs machine_logs_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.machine_logs
    ADD CONSTRAINT machine_logs_pkey PRIMARY KEY (id);


--
-- TOC entry 3306 (class 2606 OID 41123)
-- Name: machine_rules machine_rules_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.machine_rules
    ADD CONSTRAINT machine_rules_pkey PRIMARY KEY (id);


--
-- TOC entry 3309 (class 2606 OID 41133)
-- Name: machine_logs machine_logs_rule_applied_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.machine_logs
    ADD CONSTRAINT machine_logs_rule_applied_id_fkey FOREIGN KEY (rule_applied_id) REFERENCES public.machine_rules(id);


-- Completed on 2025-12-17 16:16:04

--
-- PostgreSQL database dump complete
--

