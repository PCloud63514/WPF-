using Newtonsoft.Json.Linq;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace OraganismSimulation.Oraganism.Core
{
    /// <summary>
    /// 시나리오
    /// 정보를 전달하면 그것을 기준으로 생성
    /// 정보 전달 없이 생성을 요청하면 oraganismGenerator를 기준으로 랜덤 생성.
    /// 정보를 두개 전달하면oraganismGenerator를 이용하여 둘의 속성을 합쳐서 랜덤 생성.
    /// </summary>
    public class OraganismFactory : ObservableCollection<Oraganism>, IDisposable
    {
        #region Field
        private OraganismInfoGenerator _oraganismInfoGenerator { get; set; }
        #endregion

        public OraganismFactory()
        {
            _oraganismInfoGenerator = new OraganismInfoGenerator();
            Initialize();
        }

        /// <summary>
        /// 초기화
        /// </summary>
        public void Initialize()
        {
            this.Clear();
        }

        /// <summary>
        /// OraganismGenerator 를 통한 무작위 속성을 지닌 Oraganism 객체 다수 생성.
        /// </summary>
        /// <param name="cNum">Instance 생성 갯수</param>
        /// <returns></returns>
        public Oraganism[] CreateOraganisms(int cNum)
        {
            Oraganism[] oraganisms = new Oraganism[cNum];
            for(int i = 0; i < cNum; i++)
            {
                Oraganism oraganism = CreateOraganism();
                this.Add(oraganism);
                oraganisms[i] = oraganism;
            }
            
            return oraganisms;
        }
        
        /// <summary>
        /// OraganismGenerator를 통한 무작위 속성을 지닌 Oraganism 객체 생성.
        /// </summary>
        /// <returns></returns>
        public Oraganism CreateOraganism()
        {
            Oraganism oraganism = new Oraganism(_oraganismInfoGenerator.GetRandomOraganismInfo());
            return oraganism;
        }

        /// <summary>
        /// Json형식으로 저장된 정보를 내보냅니다.
        /// </summary>
        public void ExportJsonOraganismInfos()
        {
            string textJson = EncloseCollectionWithJObject().ToString();
            //TODO 나중에 GlobalValue 형식으로 Step에 대한 정보를 전달받을 수 있다면 Step이 저장 폴더 명의 기준이 될 것.
            string exportPath = Path.Combine(Environment.CurrentDirectory, "1");

            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }
            exportPath = Path.Combine(exportPath, "first.json");
            using (StreamWriter sw = new StreamWriter(File.OpenWrite(exportPath)))
            {
                sw.Write(textJson);
            }
        }

        /// <summary>
        /// 현재 컬렉션의 정보를 JObject로 감싸 반환합니다.
        /// </summary>
        /// <returns></returns>
        private JObject EncloseCollectionWithJObject()
        {
            JObject rootObject = new JObject();
            JArray jArray = new JArray();

            foreach (Oraganism oraganism in this)
            {
                jArray.Add(ConvertOraganismToJObject(oraganism));
            }
            rootObject.Add(jArray);

            return rootObject;
        }

        /// <summary>
        /// parameter를 JObject로 변환하여 반환합니다.
        /// </summary>
        /// <param name="oraganism"></param>
        /// <returns></returns>
        private JObject ConvertOraganismToJObject(Oraganism oraganism)
        {
            JObject jObject = new JObject();
            jObject.Add(nameof(oraganism.X), oraganism.X);
            jObject.Add(nameof(oraganism.Y), oraganism.Y);
            jObject.Add(nameof(oraganism.Width), oraganism.Width);
            jObject.Add(nameof(oraganism.Height), oraganism.Height);
            jObject.Add(nameof(oraganism.Value), oraganism.Value);

            return jObject;
        }

        /// <summary>
        /// Oraganism 객체 제거
        /// </summary>
        /// <param name="oraganism"></param>
        public void RemoveOraganism(Oraganism oraganism)
        {
            this.Remove(oraganism);
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Clear();
                _oraganismInfoGenerator = null;
            }
        }
        void IDisposable.Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
