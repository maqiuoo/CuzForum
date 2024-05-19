from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.common.exceptions import TimeoutException

import requests
from bs4 import BeautifulSoup

# 配置Selenium WebDriver
options = webdriver.ChromeOptions()
options.add_argument('--headless')  # 无头模式
driver = webdriver.Chrome(options=options)

# 主页面的域名，用于构建完整的书籍链接
domain = 'https://lib.cuz.edu.cn'

def get_books(main_url):
    """从主页面获取书籍链接和标题"""
    response = requests.get(main_url)
    soup = BeautifulSoup(response.content, 'html.parser')
    books = []
    for div in soup.find_all('div', class_='title'):
        a_tag = div.find('a', href=True)
        if a_tag:
            # 构建完整的书籍链接
            book_link = domain + a_tag['href'].replace('..', '')
            # print(book_link)
            # 获取书籍标题
            book_title = a_tag.text.strip()
            books.append((book_link, book_title))
    return books


def get_book_clicks(book_url):
    """使用Selenium访问书籍链接并获取点击量信息"""
    driver.get(book_url)
    try:
        # 等待JavaScript加载并显示点击量
        element_present = EC.presence_of_element_located((By.XPATH, "//span[starts-with(@id, 'dynclicks_wbnews_')]"))
        WebDriverWait(driver, 10).until(element_present)

        # 获取点击量
        clicks_element = driver.find_element(By.XPATH, "//span[starts-with(@id, 'dynclicks_wbnews_')]")
        clicks = clicks_element.text
        return clicks
    except TimeoutException:
        return "点击量信息加载超时"


def main():
    page_num = 0  # 从0开始，假设jypxb.htm相当于jypxb0.htm
    while True:
        # 构建当前页面的URL
        if page_num == 0:
            current_url = 'https://lib.cuz.edu.cn/index/jypxb.htm'  # 第一个页面
        else:
            current_url = f'https://lib.cuz.edu.cn/index/jypxb/{page_num}.htm'  # 后续页面

        # 检查页面是否存在
        response = requests.get(current_url)
        if response.status_code != 200:
            print(f'页面 {current_url} 不存在，停止遍历。')
            break  # 如果页面不存在，则退出循环

        # 页面存在，获取书籍链接和标题的列表
        books = get_books(current_url)
        for book_link, book_title in books:
            clicks = get_book_clicks(book_link)
            print(f'书籍名称: {book_title} 的点击量为: {clicks}')

        # 页面序号增加，准备访问下一个页面
        page_num += 1


if __name__ == '__main__':
    main()
    driver.quit()  # 关闭浏览器