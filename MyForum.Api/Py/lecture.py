from bs4 import BeautifulSoup
import requests
from datetime import datetime
from urllib.parse import urlparse, parse_qs, urlencode, urlunparse

base_url = 'https://oa.cuz.edu.cn/yxy/yxy-xsjz.jsp?urltype=tree.TreeTempUrl&wbtreeid=1205'

# 初始化分页参数
page_num = 0

# 当前日期
today = datetime.now().date()

T = True

lectures = []

while T:
    # 更新查询参数为当前页面
    parsed_base_url = urlparse(base_url)
    query_params = parse_qs(parsed_base_url.query)
    page_num += 1
    query_params['PAGENUM'] = str(page_num)
    new_query_string = urlencode(query_params, doseq=True)
    new_url = urlunparse((
        parsed_base_url.scheme,
        parsed_base_url.netloc,
        parsed_base_url.path,
        parsed_base_url.params,
        new_query_string,
        parsed_base_url.fragment
    ))

    response = requests.get(new_url)

    if response.status_code == 200:
        soup = BeautifulSoup(response.content, 'html.parser')

        lecture_items = soup.find_all('li')


        for item in lecture_items:
            date_div = item.find('div', {'class': 'sp-list-time rig'})

            if date_div:
                date_str = date_div.text.strip()
                lecture_date = datetime.strptime(date_str, '%Y/%m/%d').date()

                if lecture_date >= today:
                    title = item.find('a').text.strip()
                    link = item.find('a')['href'].strip()
                    full_link = f'https://oa.cuz.edu.cn/yxy/{link}'
                    lectures.append({'title': title, 'link': full_link, 'date': date_str})

                else:
                    T = False
                    # print("所有今天及未来的讲座如下：")
                    break

    else:
        print("访问页面失败，状态码为：", response.status_code)
        break

    if T == False:

        # for lecture in lectures:
        #     print(lecture)
        print(lectures)
