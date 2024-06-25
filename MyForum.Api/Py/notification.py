from bs4 import BeautifulSoup
import requests
from datetime import datetime
import re

domain = 'https://oa.cuz.edu.cn/'
# 日常通知
base_url_rctz = 'https://oa.cuz.edu.cn/rctz1.htm'
# 公示公告
base_url_gsgg =  'https://oa.cuz.edu.cn/gsgg1.htm'

def getData(base_url):
    response = requests.get(base_url)

    soup = BeautifulSoup(response.content, 'html.parser')
    affiche_items = soup.find_all('li')
    affiches = []

    T = True
    today = datetime.now().date()

    for item in affiche_items:
        date_div = item.find('div', {'class': 'sp-list-time rig'})

        if date_div:
            date_str = date_div.text.strip()
            affiche_date = datetime.strptime(date_str, '%Y-%m-%d').date()
            delta = today - affiche_date

            if delta.days <= 30:
                title = item.find('a').text.strip()
                link = item.find('a')['href'].strip()
                full_link = f'https://oa.cuz.edu.cn/{link}'
                affiches.append({'title': title, 'link': full_link, 'date': date_str})

            else:
                T = False
                # print("所有今天及未来的公告如下：")
                break

    # 查找所有的<a>标签
    links = soup.find_all('a', class_='Next')

    # 提取下一页的网址
    next_page_url = None
    for link in links:
        if link.text.strip() == '下页':
            next_page_url = link['href']
            break

    next_page_url_first = domain + next_page_url

    # print(f"第二页的网址是: {next_page_url_first}")

    # 使用正则表达式从URL中提取数字部分
    match = re.search(r'(\d+)\.htm$', next_page_url_first)
    if T and match:
        # 获取数字部分并转换为整数
        start_number = int(match.group(1))

        # 从这个数字开始向下遍历
        for i in range(start_number, 0, -1):
            # 构造新的URL
            new_url = re.sub(r'\d+\.htm$', f"{i}.htm", next_page_url_first)
            # print(new_url)
            # 尝试访问新的URL
            try:
                response = requests.get(new_url)
                # 根据HTTP响应状态码检查是否成功访问
                if response.status_code == 200:
                    # print(f"成功访问: {new_url}")
                    soup = BeautifulSoup(response.content, 'html.parser')
                    affiche_items = soup.find_all('li')
                    for item in affiche_items:
                        date_div = item.find('div', {'class': 'sp-list-time rig'})

                        if date_div:
                            date_str = date_div.text.strip()
                            affiche_date = datetime.strptime(date_str, '%Y-%m-%d').date()
                            delta = today - affiche_date

                            if delta.days <= 30:
                                title = item.find('a').text.strip()
                                link = item.find('a')['href'].strip()
                                full_link = f'https://oa.cuz.edu.cn/{link}'
                                affiches.append({'title': title, 'link': full_link, 'date': date_str})

                            else:
                                T = False
                                # print("所有今天及未来的公告如下：")
                                break

                else:
                    print(f"访问失败: {new_url}, 状态码: {response.status_code}")
            except requests.RequestException as e:
                print(f"请求错误: {e}, URL: {new_url}")

            if not T:
                break
    # else:
    #     print("无法从URL中提取数字")

    # for affiche in affiches:
    #     print(affiche)

    return affiches

rctz = getData(base_url_rctz)
gsgg = getData(base_url_gsgg)

# print(rctz)
# print(gsgg)

# 合并两个数组
combined = rctz + gsgg

# 按照'date'字段排序，越晚的日期越靠前
sorted_combined = sorted(combined, key=lambda x: x['date'], reverse=True)

# 打印排序后的合并数组
print(sorted_combined)