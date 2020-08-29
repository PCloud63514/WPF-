namespace OraganismSimulation.Oraganism.Core
{
    public class OraganismInfoGenerator
    {
        /// <summary>
        /// 랜덤 정보를 통한 OraganismInfo 인스턴스 생성 및 반환
        /// </summary>
        /// <returns></returns>
        public OraganismInfo GetRandomOraganismInfo()
        {
            double x = 0;
            double y = 0;
            double width = 0;
            double height = 0;
            double value = 0;
            OraganismInfo oraganismInfo = new OraganismInfo(x, y, width, height, value);

            return oraganismInfo;
        }
    }
}
